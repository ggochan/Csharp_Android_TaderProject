package com.ghc.td_app.Ordering;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.ghc.td_app.MainActivity;
import com.ghc.td_app.Model.One_Menu_Model;
import com.ghc.td_app.Model.One_Option_Model;
import com.ghc.td_app.Model.Ordering_public_Model;
import com.ghc.td_app.NetworkHelper;
import com.ghc.td_app.R;
import com.ghc.td_app.TakeOrder.TakeOrderAct;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class OrderingAct extends AppCompatActivity implements View.OnClickListener {

    String User_Id, Base_URL;
    String User_Update_info =""; // 유저 업데이트 정보
    public TextView update_text, ordering_kind, ordering_store;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.act_ordering);

        Intent DataIntent = getIntent();
        User_Id = DataIntent.getStringExtra("ID");
        Base_URL = DataIntent.getStringExtra("URL");

        View back_view;
        ImageView back_Image;
        TextView back_tv;
        back_view = findViewById(R.id.ordering_view);
        back_view.setOnClickListener(this);
        back_Image = findViewById(R.id.ordering_back_arrow);
        back_Image.setOnClickListener(this);
        back_tv = findViewById(R.id.ordering_title);
        back_tv.setOnClickListener(this);
        TextView update_tv;
        update_tv = findViewById(R.id.ordering_update);
        update_tv.setOnClickListener(this);

        Ordering_field();

        FloatingActionButton memo_fab_btn = findViewById(R.id.schedule_fab_btn);
        memo_fab_btn.setOnClickListener(this);

        populateList();

    }
    public void Listsetup(ArrayList<Model_Ordering> list)
    {
        ListView lview = (ListView) findViewById(R.id.ordering_listview);
        Adapter_Ordering adapter = new Adapter_Ordering(this, list);
        lview.setAdapter(adapter);

        adapter.notifyDataSetChanged();
        lview.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, View view,
                                    int position, long id) {

                if (User_Update_info.equals("")) {
                    String od_num = ((TextView) view.findViewById(R.id.adap_num)).getText().toString();
                    String od_menu = ((TextView) view.findViewById(R.id.adap_menu)).getText().toString();
                    String od_count = ((TextView) view.findViewById(R.id.adap_menu_count)).getText().toString();

                    final AlertDialog.Builder plus_dlg = new AlertDialog.Builder(OrderingAct.this, R.style.AlertDialog);
                    plus_dlg.setView(R.layout.dlg_ordering);

                    final AlertDialog alertDialog = plus_dlg.create();
                    alertDialog.show();

                    final EditText dlg_menu, dlg_option;
                    dlg_menu = alertDialog.findViewById(R.id.dlg_ordering_menu);
                    dlg_menu.setEnabled(false);
                    dlg_option = alertDialog.findViewById(R.id.dlg_ordering_option);

                    dlg_menu.setText(od_menu);

                    final Spinner dlg_menu_spi, dlg_option_spi;
                    dlg_menu_spi = alertDialog.findViewById(R.id.dlg_ordering_menu_spi);
                    dlg_menu_spi.setEnabled(false);
                    dlg_option_spi = alertDialog.findViewById(R.id.dlg_ordering_option_spi);

                    //메뉴 옵션 주문
                    Call<List<One_Option_Model>> combo_option_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboOption(ordering_store.getText().toString());
                    combo_option_up.enqueue(new Callback<List<One_Option_Model>>() {
                        @Override
                        public void onResponse(Call<List<One_Option_Model>> call, Response<List<One_Option_Model>> response) {
                            List<One_Option_Model> option_menu = response.body();

                            if (response.isSuccessful()) {
                                Log.d("콤보 옵션", "성공 / 상태 : " + option_menu.toString());
                                List<String> option_list = new ArrayList<>();
                                for (int i = 0; i < option_menu.size(); i++) {
                                    option_list.add(option_menu.get(i).getComboOption());
                                }
                                SpinnerItemAdd(option_list, dlg_option_spi, dlg_option);
                            } else {

                                Log.d("콤보 옵션", "실패");
                            }
                        }

                        @Override
                        public void onFailure(Call<List<One_Option_Model>> call, Throwable t) {
                            Log.d("콤보 옵션", "오류" + t.getMessage());
                        }
                    });
                    TextView ok_btn = alertDialog.findViewById(R.id.dlg_ordering_ok);
                    ok_btn.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            // 메뉴 옵션
                            if (!dlg_menu.getText().toString().equals("")) {
                                Call<List<String>> ordering_plus_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postMenuOption(new Ordering_public_Model(User_Id, dlg_menu.getText().toString(), dlg_option.getText().toString()));
                                ordering_plus_call.enqueue(new Callback<List<String>>() {
                                    @Override
                                    public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                                        List<String> ordering_plus_list = response.body();
                                        if (response.isSuccessful()) {
                                            Log.d("메뉴, 옵션 저장", "성공 / 상태 : " + ordering_plus_list.get(0));
                                            // main 화면 전환
                                            Log.d("Ordering 저장", "성공");
                                        } else {
                                            Log.d("메뉴, 옵션", "실패");
                                        }
                                    }

                                    @Override
                                    public void onFailure(Call<List<String>> call, Throwable t) {
                                        Log.d("메뉴, 옵션 저장", "오류" + t.getMessage());
                                    }
                                });
                                //
                                alertDialog.dismiss();
                                Ordering_field();
                                populateList();
                            } else if (dlg_menu.getText().toString().equals("")) {
                                Toast.makeText(getApplicationContext(), "메뉴가 비어있습니다.", Toast.LENGTH_LONG).show();
                            }
                        }
                    });

                    TextView cancle_btn = alertDialog.findViewById(R.id.dlg_ordering_close);
                    cancle_btn.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            alertDialog.dismiss();
                        }
                    });
                }
                else if (!User_Update_info.equals(""))
                {
                    Toast.makeText(getApplicationContext(), "이미 주문하셨습니다\n우 상단의 주문수정만 가능합니다.", Toast.LENGTH_LONG).show();
                }
            }
        });
    }
    public void populateList() {
        Call<List<Model_Ordering_list>> list_info = NetworkHelper.getInstance(Base_URL).getRetrofitService().getTakeOrderList();
        list_info.enqueue(new Callback<List<Model_Ordering_list>>() {
            @Override
            public void onResponse(Call<List<Model_Ordering_list>> call, Response<List<Model_Ordering_list>> response) {
                List<Model_Ordering_list> list_info_li = response.body();
                if (response.isSuccessful()) {
                    Log.d("주문리스트", "성공");
                    ArrayList<Model_Ordering> orderingList = new ArrayList<>();
                    for(int i=0; i<list_info_li.size(); i++)
                    {
                        orderingList.add(new Model_Ordering(String.valueOf(i+1),list_info_li.get(i).getMenu(),String.valueOf(list_info_li.get(i).getMenu_Count())));
                    }
                    Listsetup(orderingList);
                } else {

                    Log.d("주문리스트", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<Model_Ordering_list>> call, Throwable t) {
                Log.d("주문리스트", "오류" + t.getMessage());
            }
        });
    }

    public void Ordering_field() {
        update_text = findViewById(R.id.ordering_update);
        ordering_kind = findViewById(R.id.ordering_kind);
        ordering_store = findViewById(R.id.ordering_store);

        Call<List<String>> kind_store_get = NetworkHelper.getInstance(Base_URL).getRetrofitService().getTakeOrder();
        kind_store_get.enqueue(new Callback<List<String>>() {
            @SuppressLint("ResourceAsColor")
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> kind_store_get_list = response.body();
                if (response.isSuccessful()) {
                    String[] sp1 = kind_store_get_list.get(0).split("&");
                    ordering_kind.setText(sp1[0]);
                    ordering_store.setText(sp1[1]);
                } else {
                    Log.d("종류, 가게명", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("종류, 가게명", "에러");
            }
        });
        Call<List<String>> user_now_get = NetworkHelper.getInstance(Base_URL).getRetrofitService().getUserNow(User_Id);
        user_now_get.enqueue(new Callback<List<String>>() {
            @SuppressLint("ResourceAsColor")
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> user_now_get_list = response.body();
                if (response.isSuccessful() && !user_now_get_list.get(0).equals("")) {
                    User_Update_info = user_now_get_list.get(0);
                    update_text.setVisibility(View.VISIBLE);
                    Log.d("유저주문내역", "있음");
                } else {
                    Log.d("유저주문내역", "없음");
                    update_text.setVisibility(View.INVISIBLE);
                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("유저주문내역", "에러");
            }
        });
    }

    @Override
    public void onClick(View view) {
        if (view.getId() == R.id.ordering_back_arrow || view.getId() == R.id.ordering_view || view.getId() == R.id.ordering_title) {
            Intent intent = new Intent(OrderingAct.this, MainActivity.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
        }
        if(view.getId() == R.id.schedule_fab_btn)
        {
            if(User_Update_info.equals("")) {
                final AlertDialog.Builder fab_dlg = new AlertDialog.Builder(OrderingAct.this, R.style.AlertDialog);
                fab_dlg.setView(R.layout.dlg_ordering);

                final AlertDialog alertDialog = fab_dlg.create();
                alertDialog.show();

                final EditText dlg_menu, dlg_option;
                dlg_menu = alertDialog.findViewById(R.id.dlg_ordering_menu);
                dlg_option = alertDialog.findViewById(R.id.dlg_ordering_option);

                final Spinner dlg_menu_spi, dlg_option_spi;
                dlg_menu_spi = alertDialog.findViewById(R.id.dlg_ordering_menu_spi);
                dlg_option_spi = alertDialog.findViewById(R.id.dlg_ordering_option_spi);

                //메뉴 옵션 주문
                Call<List<One_Menu_Model>> combo_menu_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboMenu(ordering_store.getText().toString());
                combo_menu_up.enqueue(new Callback<List<One_Menu_Model>>() {
                    @Override
                    public void onResponse(Call<List<One_Menu_Model>> call, Response<List<One_Menu_Model>> response) {
                        List<One_Menu_Model> combo_menu = response.body();

                        if (response.isSuccessful()) {
                            Log.d("콤보 메뉴", "성공 / 상태 : " + combo_menu.toString());
                            List<String> menu_list = new ArrayList<>();
                            for (int i = 0; i < combo_menu.size(); i++) {
                                menu_list.add(combo_menu.get(i).getComboMenu());
                            }
                            SpinnerItemAdd(menu_list, dlg_menu_spi, dlg_menu);
                            dlg_menu.requestFocus();
                        } else {

                            Log.d("콤보 메뉴", "실패");
                        }
                    }

                    @Override
                    public void onFailure(Call<List<One_Menu_Model>> call, Throwable t) {
                        Log.d("콤보 메뉴", "오류" + t.getMessage());
                    }
                });
                Call<List<One_Option_Model>> combo_option_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboOption(ordering_store.getText().toString());
                combo_option_up.enqueue(new Callback<List<One_Option_Model>>() {
                    @Override
                    public void onResponse(Call<List<One_Option_Model>> call, Response<List<One_Option_Model>> response) {
                        List<One_Option_Model> option_menu = response.body();

                        if (response.isSuccessful()) {
                            Log.d("콤보 옵션", "성공 / 상태 : " + option_menu.toString());
                            List<String> option_list = new ArrayList<>();
                            for (int i = 0; i < option_menu.size(); i++) {
                                option_list.add(option_menu.get(i).getComboOption());
                            }
                            SpinnerItemAdd(option_list, dlg_option_spi, dlg_option);
                        } else {

                            Log.d("콤보 옵션", "실패");
                        }
                    }

                    @Override
                    public void onFailure(Call<List<One_Option_Model>> call, Throwable t) {
                        Log.d("콤보 옵션", "오류" + t.getMessage());
                    }
                });
                //
                TextView ok_btn = alertDialog.findViewById(R.id.dlg_ordering_ok);
                ok_btn.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        // 메뉴 옵션
                        if (!dlg_menu.getText().toString().equals("")) {
                            Call<List<String>> ordering_update_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postMenuOption(new Ordering_public_Model(User_Id, dlg_menu.getText().toString(), dlg_option.getText().toString()));
                            ordering_update_call.enqueue(new Callback<List<String>>() {
                                @Override
                                public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                                    List<String> ordering_up_list = response.body();
                                    if (response.isSuccessful()) {
                                        Log.d("메뉴, 옵션 저장", "성공 / 상태 : " + ordering_up_list.get(0));
                                        // main 화면 전환
                                        Log.d("Ordering 저장", "성공");
                                    } else {
                                        Log.d("메뉴, 옵션", "실패");
                                    }
                                }

                                @Override
                                public void onFailure(Call<List<String>> call, Throwable t) {
                                    Log.d("메뉴, 옵션 저장", "오류" + t.getMessage());
                                }
                            });
                            //
                            alertDialog.dismiss();
                            Ordering_field();
                            populateList();
                        }
                        else if(dlg_menu.getText().toString().equals(""))
                        {
                            Toast.makeText(getApplicationContext(), "메뉴가 비어있습니다.", Toast.LENGTH_LONG).show();
                        }
                    }
                });

                TextView cancle_btn = alertDialog.findViewById(R.id.dlg_ordering_close);
                cancle_btn.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        alertDialog.dismiss();
                    }
                });
            }
            else if(!User_Update_info.equals(""))
            {
                Toast.makeText(getApplicationContext(), "이미 주문하셨습니다\n우 상단의 주문수정만 가능합니다.", Toast.LENGTH_LONG).show();
            }
        }
        if(view.getId() == R.id.ordering_update)
        {
            if(!User_Update_info.equals(""))
            {
                final AlertDialog.Builder update_dlg = new AlertDialog.Builder(OrderingAct.this, R.style.AlertDialog);
                update_dlg.setView(R.layout.dlg_ordering);

                final AlertDialog alertDialog = update_dlg.create();
                alertDialog.show();

                final TextView ordering_title;
                ordering_title = alertDialog.findViewById(R.id.ordering_title_text);
                ordering_title.setText("메뉴 수정");

                final EditText dlg_menu, dlg_option;
                dlg_menu = alertDialog.findViewById(R.id.dlg_ordering_menu);
                dlg_option = alertDialog.findViewById(R.id.dlg_ordering_option);

                String[] sp2 = User_Update_info.split("&");
                dlg_menu.setText(sp2[0]);
                dlg_option.setText(sp2[1]);

                final Spinner dlg_menu_spi, dlg_option_spi;
                dlg_menu_spi = alertDialog.findViewById(R.id.dlg_ordering_menu_spi);
                dlg_option_spi = alertDialog.findViewById(R.id.dlg_ordering_option_spi);

                //메뉴 옵션 주문
                Call<List<One_Menu_Model>> combo_menu_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboMenu(ordering_store.getText().toString());
                combo_menu_up.enqueue(new Callback<List<One_Menu_Model>>() {
                    @Override
                    public void onResponse(Call<List<One_Menu_Model>> call, Response<List<One_Menu_Model>> response) {
                        List<One_Menu_Model> combo_menu = response.body();

                        if (response.isSuccessful()) {
                            Log.d("콤보 메뉴", "성공 / 상태 : " + combo_menu.toString());
                            List<String> menu_list = new ArrayList<>();
                            for (int i = 0; i < combo_menu.size(); i++) {
                                menu_list.add(combo_menu.get(i).getComboMenu());
                            }
                            SpinnerItemAdd(menu_list, dlg_menu_spi, dlg_menu);
                        } else {

                            Log.d("콤보 메뉴", "실패");
                        }
                    }

                    @Override
                    public void onFailure(Call<List<One_Menu_Model>> call, Throwable t) {
                        Log.d("콤보 메뉴", "오류" + t.getMessage());
                    }
                });
                Call<List<One_Option_Model>> combo_option_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboOption(ordering_store.getText().toString());
                combo_option_up.enqueue(new Callback<List<One_Option_Model>>() {
                    @Override
                    public void onResponse(Call<List<One_Option_Model>> call, Response<List<One_Option_Model>> response) {
                        List<One_Option_Model> option_menu = response.body();

                        if (response.isSuccessful()) {
                            Log.d("콤보 옵션", "성공 / 상태 : " + option_menu.toString());
                            List<String> option_list = new ArrayList<>();
                            for (int i = 0; i < option_menu.size(); i++) {
                                option_list.add(option_menu.get(i).getComboOption());
                            }
                            SpinnerItemAdd_option(option_list, dlg_option_spi, dlg_option);
                        } else {

                            Log.d("콤보 옵션", "실패");
                        }
                    }

                    @Override
                    public void onFailure(Call<List<One_Option_Model>> call, Throwable t) {
                        Log.d("콤보 옵션", "오류" + t.getMessage());
                    }
                });
                TextView ok_btn = alertDialog.findViewById(R.id.dlg_ordering_ok);
                ok_btn.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        // 메뉴 옵션
                        if (!dlg_menu.getText().toString().equals("")) {
                            Call<List<String>> ordering_update_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postMenuUpdate(new Ordering_public_Model(User_Id, dlg_menu.getText().toString(), dlg_option.getText().toString()));
                            ordering_update_call.enqueue(new Callback<List<String>>() {
                                @Override
                                public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                                    List<String> ordering_up_list = response.body();
                                    if (response.isSuccessful()) {
                                        Log.d("메뉴, 옵션 업데이트", "성공 / 상태 : " + ordering_up_list.get(0));
                                        // main 화면 전환
                                        Log.d("Ordering 업데이트", "성공");
                                    } else {
                                        Log.d("메뉴, 옵션", "실패");
                                    }
                                }

                                @Override
                                public void onFailure(Call<List<String>> call, Throwable t) {
                                    Log.d("메뉴, 옵션 업데이트", "오류" + t.getMessage());
                                }
                            });
                            //
                            alertDialog.dismiss();
                            Ordering_field();
                            populateList();
                        }
                        else if(dlg_menu.getText().toString().equals(""))
                        {
                            Toast.makeText(getApplicationContext(), "메뉴가 비어있습니다.", Toast.LENGTH_LONG).show();
                        }
                    }
                });

                TextView cancle_btn = alertDialog.findViewById(R.id.dlg_ordering_close);
                cancle_btn.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        alertDialog.dismiss();
                    }
                });
            }
            else if(User_Update_info.equals(""))
            {
                Toast.makeText(getApplicationContext(), "현재 주문 내역이 없습니다.", Toast.LENGTH_LONG).show();
            }
        }
    }
    public void SpinnerItemAdd(List<String> addstr, Spinner spinner_val, final EditText edittext_val) {
        ArrayAdapter<String> kind_spinnerAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, addstr);
        kind_spinnerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        kind_spinnerAdapter.notifyDataSetChanged();

        spinner_val.setAdapter(kind_spinnerAdapter);
        spinner_val.setSelection(0, false);
        spinner_val.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                edittext_val.setText(adapterView.getSelectedItem().toString());
                edittext_val.requestFocus();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
                Toast.makeText(getApplicationContext(), "사용 가능한 ID가 아닙니다.", Toast.LENGTH_SHORT).show();
            }
        });
    }
    public void SpinnerItemAdd_option(List<String> addstr, Spinner spinner_val, final EditText edittext_val) {
        ArrayAdapter<String> kind_spinnerAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, addstr);
        kind_spinnerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        kind_spinnerAdapter.notifyDataSetChanged();

        spinner_val.setAdapter(kind_spinnerAdapter);
        spinner_val.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                edittext_val.setText(adapterView.getSelectedItem().toString());
                edittext_val.requestFocus();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
                Toast.makeText(getApplicationContext(), "사용 가능한 ID가 아닙니다.", Toast.LENGTH_SHORT).show();
            }
        });
    }
}


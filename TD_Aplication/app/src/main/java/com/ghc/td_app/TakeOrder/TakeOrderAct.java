package com.ghc.td_app.TakeOrder;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.SpinnerAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.ghc.td_app.MainActivity;
import com.ghc.td_app.Model.One_Kind_Model;
import com.ghc.td_app.Model.One_Menu_Model;
import com.ghc.td_app.Model.One_Option_Model;
import com.ghc.td_app.Model.One_Store_Model;
import com.ghc.td_app.NetworkHelper;
import com.ghc.td_app.Model.Ordering_public_Model;
import com.ghc.td_app.R;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TakeOrderAct extends AppCompatActivity implements View.OnClickListener {
    String User_Id, Base_URL;
    int User_Count = 0;
    Context mContext;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.act_takeorder);
        mContext = this;

        Intent DataIntent = getIntent();
        User_Id = DataIntent.getStringExtra("ID");
        Base_URL = DataIntent.getStringExtra("URL");

        View back_view1;
        ImageView back_Image;
        TextView back_tv;
        back_view1 = findViewById(R.id.takeorder_view1);
        back_view1.setOnClickListener(this);
        back_Image = findViewById(R.id.takeorder_back_arrow);
        back_Image.setOnClickListener(this);
        back_tv = findViewById(R.id.takeorder_title);
        back_tv.setOnClickListener(this);


        final EditText Et_kind, Et_store, Et_menu, Et_option;
        Et_kind = findViewById(R.id.takeorder_kind);
        Et_store = findViewById(R.id.takeorder_store);
        Et_menu = findViewById(R.id.takeorder_menu);
        Et_option = findViewById(R.id.takeorder_option);

        TextView storge_tv;
        storge_tv = findViewById(R.id.takeorder_storage);
        storge_tv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (!Et_kind.equals("") && !Et_store.equals("") && !Et_menu.equals("") && !Et_option.equals("") && User_Count != 0) {
                    Call<List<String>> tdupdate_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postTakeOrder(new TakeOrder_Update_Model(Et_kind.getText().toString(), Et_store.getText().toString(), User_Count));
                    tdupdate_call.enqueue(new Callback<List<String>>() {
                        @Override
                        public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                            List<String> td_up_list = response.body();
                            if (response.isSuccessful()) {
                                Log.d("종류, 가게명 저장", "성공" + td_up_list.get(0));
                                // 메뉴 옵션
                                Call<List<String>> ordering_update_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postMenuOption(new Ordering_public_Model(User_Id, Et_menu.getText().toString(), Et_option.getText().toString()));
                                ordering_update_call.enqueue(new Callback<List<String>>() {
                                    @Override
                                    public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                                        List<String> ordering_up_list = response.body();
                                        if (response.isSuccessful()) {
                                            Log.d("메뉴, 옵션 저장", "성공 / 상태 : " + ordering_up_list.get(0));
                                            // main 화면 전환
                                            Log.d("TakeOrder 저장", "성공");
                                            Intent intent = new Intent(TakeOrderAct.this, MainActivity.class);
                                            startActivity(intent);
                                            overridePendingTransition(0, 0);
                                            finish();
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
                            } else {
                                Log.d("종류, 가게명 저장", "실패");
                            }
                        }

                        @Override
                        public void onFailure(Call<List<String>> call, Throwable t) {
                            Log.d("종류, 가게명 저장", "오류" + t.getMessage());
                        }
                    });
                }
                else
                {
                    Toast.makeText(getApplicationContext(), "종류, 가게명, 메뉴, 옵션, 인원 수를 확인하세요.", Toast.LENGTH_SHORT).show();
                }
            }
        });

        final Spinner spinner1, spinner2, spinner3, spinner4, spinner5;
        spinner1 = findViewById(R.id.takeorder_kindspi);
        spinner2 = findViewById(R.id.takeorder_storespi);
        spinner3 = findViewById(R.id.takeorder_menuspi);
        spinner4 = findViewById(R.id.takeorder_optionspi);
        spinner5 = findViewById(R.id.takeorder_users);

        // 종류
        Call<List<One_Kind_Model>> combo_kind_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboKind();
        combo_kind_up.enqueue(new Callback<List<One_Kind_Model>>() {
            @Override
            public void onResponse(Call<List<One_Kind_Model>> call, Response<List<One_Kind_Model>> response) {
                List<One_Kind_Model> combo_kind = response.body();

                if (response.isSuccessful()) {
                    Log.d("콤보 종류", "성공 / 상태 : " + combo_kind.toString());
                    List<String> kindlist = new ArrayList<>();
                    for (int i = 0; i < combo_kind.size(); i++) {
                        kindlist.add(combo_kind.get(i).getComboKind());
                    }
                    SpinnerItemAdd(kindlist, spinner1, Et_kind);
                    Et_kind.requestFocus();
                } else {

                    Log.d("콤보 종류", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<One_Kind_Model>> call, Throwable t) {
                Log.d("콤보 종류", "오류" + t.getMessage());
            }
        });
        // 가게명
        Et_kind.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            @Override
            public void onFocusChange(View view, boolean b) {
                if (b) {
                    if(Et_kind.equals(""))
                    {
                        Toast.makeText(getApplicationContext(), "종류를 먼저 입력해주세요.", Toast.LENGTH_SHORT).show();
                        Et_kind.requestFocus();
                    }
                    SpinnerSetup(spinner2);
                    SpinnerSetup(spinner3);
                    SpinnerSetup(spinner4);
                    Et_store.setText("");
                    Et_menu.setText("");
                    Et_option.setText("");
                } else {
                    if(!Et_kind.equals("")) {
                        Call<List<One_Store_Model>> combo_store_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboStore(Et_kind.getText().toString());
                        combo_store_up.enqueue(new Callback<List<One_Store_Model>>() {
                            @Override
                            public void onResponse(Call<List<One_Store_Model>> call, Response<List<One_Store_Model>> response) {
                                List<One_Store_Model> combo_store = response.body();

                                if (response.isSuccessful()) {
                                    Log.d("콤보 가게명", "성공 / 상태 : " + combo_store.toString());
                                    List<String> store_list = new ArrayList<>();
                                    for (int i = 0; i < combo_store.size(); i++) {
                                        store_list.add(combo_store.get(i).getComboStore());
                                    }
                                    SpinnerItemAdd(store_list, spinner2, Et_store);
                                    Et_store.requestFocus();
                                } else {

                                    Log.d("콤보 가게명", "실패");
                                }
                            }

                            @Override
                            public void onFailure(Call<List<One_Store_Model>> call, Throwable t) {
                                Log.d("콤보 가게명", "오류" + t.getMessage());
                            }
                        });
                    }
                }
            }
        });
        //메뉴
        Et_store.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            @Override
            public void onFocusChange(View view, boolean b) {
                if (b) {
                    if(Et_store.equals(""))
                    {
                        Toast.makeText(getApplicationContext(), "가게명을 먼저 입력해주세요.", Toast.LENGTH_SHORT).show();
                        Et_store.requestFocus();
                    }
                    SpinnerSetup(spinner3);
                    SpinnerSetup(spinner4);
                    Et_menu.setText("");
                    Et_option.setText("");
                }
                else {
                    if(!Et_store.equals("")) {
                        Call<List<One_Menu_Model>> combo_menu_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboMenu(Et_store.getText().toString());
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
                                    SpinnerItemAdd(menu_list, spinner3, Et_menu);
                                    Et_menu.requestFocus();
                                } else {

                                    Log.d("콤보 메뉴", "실패");
                                }
                            }

                            @Override
                            public void onFailure(Call<List<One_Menu_Model>> call, Throwable t) {
                                Log.d("콤보 메뉴", "오류" + t.getMessage());
                            }
                        });
                        Call<List<One_Option_Model>> combo_option_up = NetworkHelper.getInstance(Base_URL).getRetrofitService().getComboOption(Et_store.getText().toString());
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
                                    SpinnerItemAdd(option_list, spinner4, Et_option);
                                } else {

                                    Log.d("콤보 옵션", "실패");
                                }
                            }

                            @Override
                            public void onFailure(Call<List<One_Option_Model>> call, Throwable t) {
                                Log.d("콤보 옵션", "오류" + t.getMessage());
                            }
                        });
                    }
                }
            }
        });
        User_counts(spinner5);
    }

    @Override
    public void onClick(View view) {
        if (view.getId() == R.id.takeorder_back_arrow || view.getId() == R.id.takeorder_view1 || view.getId() == R.id.takeorder_title) {
            AlertDialog.Builder td_dlg = new AlertDialog.Builder(TakeOrderAct.this, R.style.AlertDialog);
            td_dlg.setView(R.layout.takeorder_dlg_check);

            final AlertDialog alertDialog = td_dlg.create();
            alertDialog.show();
            ;

            TextView ok_btn = alertDialog.findViewById(R.id.dlg_takeorder_ok);
            ok_btn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Log.d("takeorder_back", "성공");
                    alertDialog.dismiss();
                    Intent intent = new Intent(TakeOrderAct.this, MainActivity.class);
                    startActivity(intent);
                    overridePendingTransition(0, 0);
                    finish();
                }
            });

            TextView cancle_btn = alertDialog.findViewById(R.id.dlg_takeorder_close);
            cancle_btn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Log.d("takeorder_back", "성공");
                    alertDialog.dismiss();
                }
            });
        }
    }
    public void User_counts(Spinner user_spinner)
    {
        ArrayList<String> items_list = new ArrayList<>();
        for(int i=2; i<31; i++)
        {
            items_list.add(i+"");
        }
        ArrayAdapter<String> items_adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, items_list);
        user_spinner.setAdapter(items_adapter);
        user_spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                User_Count = Integer.parseInt(adapterView.getSelectedItem().toString());
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
    }
    public void SpinnerSetup(Spinner spinner_val) {
        List<String> basic_list = new ArrayList<>();
        ArrayAdapter<String> kind_spinnerAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, basic_list);
        kind_spinnerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        kind_spinnerAdapter.notifyDataSetChanged();

        spinner_val.setAdapter(kind_spinnerAdapter);
        spinner_val.setSelection(0, false);
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
}

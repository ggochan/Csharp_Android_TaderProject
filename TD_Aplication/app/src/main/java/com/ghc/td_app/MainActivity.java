package com.ghc.td_app;

import android.annotation.SuppressLint;
import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.Build;
import android.support.v4.app.NotificationCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.text.TextUtils;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.ghc.td_app.Model.Past_All_Model;
import com.ghc.td_app.Model.Voting_All;
import com.ghc.td_app.Model.Voting_Content;
import com.ghc.td_app.Model.Voting_Title;
import com.ghc.td_app.Model.Voting_User;
import com.ghc.td_app.Ordering.OrderingAct;
import com.ghc.td_app.Setting.SettingAct;
import com.ghc.td_app.TakeOrder.TakeOrderAct;
import com.ghc.td_app.Voting.VotingBoxAct;
import com.ghc.td_app.Voting.VotingUserAct;

import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Collections;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {
    public String base_url = "";
    public TextView tb_main_id, tb_takeorder_tv, tb_voting_tv, tb_take1_tv,tb_take2_tv, tb_voting1, tb_voting2;
    public TextView field_kind, field_store, field_menu, field_option;
    public LinearLayout ll_main_tab1, ll_main_tab2;
    public RecyclerView recyclerView, recyclerView2;
    public TextView field_title, field_memo,field_day,field_content,field_count,field_allcount,field_user;
    public String check = "0";
    public Button voting_end_btn, takeorder_end_btn;
    Context mcontext;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mcontext = this;
        Intent DataIntent = getIntent();
        check = DataIntent.getStringExtra("Check");

        //바인딩선언
        tb_main_id = findViewById(R.id.main_id_tb); // ID TEXT
        tb_takeorder_tv = findViewById(R.id.main_tab1_text); // TAB TEXT
        tb_voting_tv = findViewById(R.id.main_tab2_text);
        ll_main_tab1 = findViewById(R.id.main_tab1); // TAB Layout
        ll_main_tab2 = findViewById(R.id.main_tab2);

        //파일처리
        try {
            FileInputStream main_name_fis = openFileInput("settingname.dat");
            DataInputStream main_name_dis = new DataInputStream(main_name_fis);
            String main_name_data = main_name_dis.readUTF();
            tb_main_id.setText(main_name_data);
            main_name_dis.close();

        } catch (FileNotFoundException e) {
            Intent intent = new Intent(MainActivity.this, SettingAct.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
        } catch (IOException e) {
            Intent intent = new Intent(MainActivity.this, SettingAct.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
            e.printStackTrace();

        }
        try {
            FileInputStream main_ip_fis = openFileInput("settingip.dat");
            DataInputStream main_ip_dis = new DataInputStream(main_ip_fis);
            String main_ip_data = main_ip_dis.readUTF();
            base_url = main_ip_data;
            main_ip_dis.close();

        } catch (FileNotFoundException e) {
            Intent intent = new Intent(MainActivity.this, SettingAct.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
        } catch (IOException e) {
            Intent intent = new Intent(MainActivity.this, SettingAct.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
            e.printStackTrace();
        }
        // ID 확인
        if (TextUtils.isEmpty(tb_main_id.getText())) {
            Intent intent = new Intent(MainActivity.this, SettingAct.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
        }
        //함수선언
        if(check == null)
        {
            TakeOrderField();
        }
        else if(check.equals("0") ) {
            tb_takeorder_tv.setTextColor(Color.WHITE);
            tb_voting_tv.setTextColor(Color.GRAY);
            TakeOrderField();
        }
        else if(check.equals("1"))
        {
            tb_takeorder_tv.setTextColor(Color.GRAY);
            tb_voting_tv.setTextColor(Color.WHITE);
            VotingBoxField();
            check="0";
        }
        /* MainRunnableThread mainRunnableThread = new MainRunnableThread();
        Thread mainthread = new Thread(mainRunnableThread);
        mainthread.start();*/
        ll_main_tab1.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("ResourceAsColor")
            @Override
            public void onClick(View view) {
                tb_takeorder_tv.setTextColor(Color.WHITE);
                tb_voting_tv.setTextColor(Color.GRAY);
                TakeOrderField();

            }
        });
        ll_main_tab2.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("ResourceAsColor")
            @Override
            public void onClick(View view) {
                tb_takeorder_tv.setTextColor(Color.GRAY);
                tb_voting_tv.setTextColor(Color.WHITE);
                VotingBoxField();
            }
        });

        // 세팅
        ImageView setting_btn;
        setting_btn = findViewById(R.id.main_setting);
        setting_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, SettingAct.class);
                startActivity(intent);
                overridePendingTransition(0, 0);
                finish();
            }
        });

    }

    // Load 함수
    public void Start_main() {

    }

    // 주문내역
    public void TakeOrderField() {
        LinearLayout linear_layout1 = findViewById(R.id.main_Takeorder_linear1); // 주문내역 레이아웃
        linear_layout1.removeAllViews();
        LayoutInflater infla_layout1 = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        infla_layout1.inflate(R.layout.main_takeorder, linear_layout1, true);

        DataField();

        LinearLayout linear_field1 = findViewById(R.id.main_takeorder_field); // 주문내역 레이아웃
        linear_field1.removeAllViews();
        LayoutInflater infla_field1 = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        infla_field1.inflate(R.layout.order_filed, linear_field1, true);

        // 하단 버튼
        tb_take1_tv = findViewById(R.id.main_btn1_text_takeorder); // Bottom Btn Text
        tb_take1_tv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, TakeOrderAct.class);
                intent.putExtra("ID",tb_main_id.getText());
                intent.putExtra("URL",base_url);
                startActivity(intent);
                overridePendingTransition(0, 0);
                finish();
            }
        });

        tb_take2_tv = findViewById(R.id.main_btn2_text_takeorder);
        tb_take2_tv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, OrderingAct.class);
                intent.putExtra("ID",tb_main_id.getText());
                intent.putExtra("URL",base_url);
                startActivity(intent);
                overridePendingTransition(0, 0);
                finish();
            }
        });
        // 주문 받기 체크
        Call<List<String>> list_check_call = NetworkHelper.getInstance(base_url).getRetrofitService().getOrderList("t");
        list_check_call.enqueue(new Callback<List<String>>() {
            @SuppressLint("ResourceAsColor")
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> stm = response.body();
                if (response.isSuccessful()) {
                    if(stm.get(0).equals("t"))
                    {
                        tb_take1_tv.setTextColor(Color.GRAY);
                        tb_take2_tv.setTextColor(Color.WHITE);
                        tb_take1_tv.setEnabled(false);
                        tb_take2_tv.setEnabled(true);
                        Log.d("listcheck", "주문있음");
                    }
                    else if(stm.get(0).equals("f"))
                    {
                        tb_take1_tv.setTextColor(Color.WHITE);
                        tb_take2_tv.setTextColor(Color.GRAY);
                        tb_take1_tv.setEnabled(true);
                        tb_take2_tv.setEnabled(false);
                        Log.d("listcheck", "주문없음");
                    }
                }
                else {
                    Log.d("listcheck", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("listcheck", "에러");
                Toast.makeText(getApplicationContext(), "서버가 연결되어있지 않습니다.", Toast.LENGTH_SHORT).show();
            }
        });
    }
    public void DataField()
    {
        //카운트 도달
        Call<List<String>> end_call = NetworkHelper.getInstance(base_url).getRetrofitService().GetCountEnd();
        end_call.enqueue(new Callback<List<String>>() {
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> filed_list = response.body();
                try {
                    if (response.isSuccessful() && !filed_list.get(0).equals("")) {
                        if (filed_list.get(0).equals("zopweiqushdzasdwqfngl")) {
                            takeorder_end_btn = findViewById(R.id.takeorder_end_btn);
                            takeorder_end_btn.setVisibility(View.INVISIBLE);
                            takeorder_end_btn.setOnClickListener((View.OnClickListener) mcontext);

                        } else {
                            takeorder_end_btn = findViewById(R.id.takeorder_end_btn);
                            takeorder_end_btn.setVisibility(View.VISIBLE);
                            takeorder_end_btn.setOnClickListener((View.OnClickListener) mcontext);
                        }

                    } else {
                        Log.d("end_count", "실패");
                        takeorder_end_btn = findViewById(R.id.takeorder_end_btn);
                        takeorder_end_btn.setVisibility(View.INVISIBLE);
                        takeorder_end_btn.setOnClickListener((View.OnClickListener) mcontext);
                    }
                }
                catch (Exception e)
                {

                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("end_count", "에러");
                try {
                    takeorder_end_btn = findViewById(R.id.takeorder_end_btn);
                    takeorder_end_btn.setVisibility(View.INVISIBLE);
                }
                catch (Exception e)
                {

                }
            }
        });
        // 현재 주문 내역 Filed, 메뉴 + 옵션
        Call<List<String>> filed_list_call1 = NetworkHelper.getInstance(base_url).getRetrofitService().getTakeOrder();
        filed_list_call1.enqueue(new Callback<List<String>>() {
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> filed_list1 = response.body();
                if (response.isSuccessful() && !filed_list1.get(0).equals("")) {
                    String[] filed_sp = filed_list1.get(0).split("&");
                    field_kind = findViewById(R.id.filed_kind_name);
                    field_store = findViewById(R.id.filed_store_name);
                    field_kind.setText(filed_sp[0]);
                    field_store.setText(filed_sp[1]);

                    /*Intent intent = new Intent(MainActivity.this, OrderingAct.class);
                    intent.putExtra("ID",tb_main_id.getText());
                    intent.putExtra("URL",base_url);
                    startActivity(intent);
                    overridePendingTransition(0, 0);
                    finish();
                    createNotificationChannel("DEFAULT","default channel", NotificationManager.IMPORTANCE_HIGH);
                    createNotification("DEFAULT",1,"주문내역이 있습니다.","종류 : "+filed_sp[0] + "   가게명 : " + filed_sp[1],intent);*/
                }
                else {
                    Log.d("filed_list_1", "실패");
                    try {
                        field_kind = findViewById(R.id.filed_kind_name);
                        field_store = findViewById(R.id.filed_store_name);
                        field_kind.setText(" ");
                        field_store.setText(" ");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("filed_list_1", "에러");
                try {
                    field_kind = findViewById(R.id.filed_kind_name);
                    field_store = findViewById(R.id.filed_store_name);
                    field_kind.setText(" ");
                    field_store.setText(" ");
                }
                catch (Exception e)
                {

                }
            }
        });
        // 현재 주문 내역 Filed, 메뉴 + 옵션
        Call<List<String>> filed_list_call2 = NetworkHelper.getInstance(base_url).getRetrofitService().getOrderlistNow();
        filed_list_call2.enqueue(new Callback<List<String>>() {
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> filed_list2 = response.body();
                if (response.isSuccessful() && !filed_list2.get(0).equals("")) {
                    Log.d("filed_list_2", filed_list2.get(0));
                    String[] filed_sp = filed_list2.get(0).split("&");
                    String menu="", option="";
                    for(int i=0; i<filed_sp.length; i++)
                    {
                        if(i % 2 == 0)
                        {
                            if(filed_sp[i].equals("비어질 메뉴"))
                            {
                                menu += " ";
                            }
                            else if(!filed_sp[i].equals("비어질 메뉴"))
                            {
                                menu += filed_sp[i];
                            }
                                menu += "\n";

                        }
                        else if(i % 2 != 0)
                        {
                            option += filed_sp[i];
                                option += "\n";
                        }
                    }
                    field_menu = findViewById(R.id.filed_menu);
                    field_option = findViewById(R.id.filed_option);
                    field_menu.setText(menu);
                    field_option.setText(option);
                }
                else {
                    Log.d("filed_list_2", "실패");
                    try {
                        field_menu = findViewById(R.id.filed_menu);
                        field_option = findViewById(R.id.filed_option);
                        field_menu.setText(" ");
                        field_option.setText(" ");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("filed_list_2", "에러");
                try {
                    field_menu = findViewById(R.id.filed_menu);
                    field_option = findViewById(R.id.filed_option);
                    field_menu.setText(" ");
                    field_option.setText(" ");
                }
                catch (Exception e)
                {

                }
            }
        });

        // 과거 주문 내역
        String td = "PastUserName&"+tb_main_id.getText();
        Call<List<Past_All_Model>> past_list_call = NetworkHelper.getInstance(base_url).getRetrofitService().getPastList(td);
        past_list_call.enqueue(new Callback<List<Past_All_Model>>() {
            @Override
            public void onResponse(Call<List<Past_All_Model>> call, Response<List<Past_All_Model>> response) {
                List<Past_All_Model> pa_list = response.body();
                try {
                    if (response.isSuccessful() && !pa_list.get(0).equals("")) {
                        Log.d("과거 내역", pa_list.get(0).getPastKindName());
                        PastAdd(pa_list);
                    } else {
                        Log.d("과거 내역", "실패");

                    }
                }
                catch (Exception e)
                {

                }
            }

            @Override
            public void onFailure(Call<List<Past_All_Model>> call, Throwable t) {
                Log.d("과거 내역", "에러");
            }
        });

    }
    //투표함
    public void VotingBoxField()
    {
        LinearLayout linear_layout1 = findViewById(R.id.main_Takeorder_linear1); // 주문내역 레이아웃
        linear_layout1.removeAllViews();
        LayoutInflater infla_layout2 = (LayoutInflater)getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        infla_layout2.inflate(R.layout.main_votingbox,linear_layout1,true);

        VotingDatafield();

        LinearLayout linear_field2 = findViewById(R.id.main_voting_field); // 투표함 레이아웃
        linear_field2.removeAllViews();
        LayoutInflater infla_field2 = (LayoutInflater)getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        infla_field2.inflate(R.layout.votingbox_filed,linear_field2,true);

        // 하단 버튼
        tb_voting1 = findViewById(R.id.main_btn1_text_votingbox); // Bottom Btn Text
        tb_voting1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, VotingBoxAct.class);
                intent.putExtra("ID",tb_main_id.getText());
                intent.putExtra("URL",base_url);
                startActivity(intent);
                overridePendingTransition(0, 0);
                finish();
            }
        });

        tb_voting2 = findViewById(R.id.main_btn2_text_votingbox);
        tb_voting2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, VotingUserAct.class);
                intent.putExtra("ID",tb_main_id.getText());
                intent.putExtra("URL",base_url);
                startActivity(intent);
                overridePendingTransition(0, 0);
                finish();
            }
        });
        voting_end_btn = findViewById(R.id.voting_end_btn);
        voting_end_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Call<List<String>> voting_end_call = NetworkHelper.getInstance(base_url).getRetrofitService().GetVotingListInsert();
                voting_end_call.enqueue(new Callback<List<String>>() {
                    @SuppressLint("ResourceAsColor")
                    @Override
                    public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                        List<String> stm = response.body();
                        if (response.isSuccessful()) {
                                Log.d("voting_end", "마감완료");
                            VotingBoxField();
                        }
                        else {
                            Log.d("voting_end", "마감실패");
                        }
                    }

                    @Override
                    public void onFailure(Call<List<String>> call, Throwable t) {
                        Log.d("voting_end", "에러");
                    }
                });
            }
        });
        //투표내역 확인
        Call<List<String>> voting_check_call = NetworkHelper.getInstance(base_url).getRetrofitService().getVotingbool();
        voting_check_call.enqueue(new Callback<List<String>>() {
            @SuppressLint("ResourceAsColor")
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> stm = response.body();
                if (response.isSuccessful()) {
                    if(stm.get(0).equals("t"))
                    {
                        tb_voting1.setTextColor(Color.GRAY);
                        tb_voting2.setTextColor(Color.WHITE);
                        tb_voting1.setEnabled(false);
                        tb_voting2.setEnabled(true);
                        Log.d("votingcheck", "투표있음");
                    }
                    else if(stm.get(0).equals("f"))
                    {
                        tb_voting1.setTextColor(Color.WHITE);
                        tb_voting2.setTextColor(Color.GRAY);
                        tb_voting1.setEnabled(true);
                        tb_voting2.setEnabled(false);
                        Log.d("votingcheck", "투표없음");
                    }
                }
                else {
                    Log.d("votingcheck", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("votingcheck", "에러");
            }
        });
    }
    public void VotingDatafield()
    {
        // 현재 투표 내역 타이틀 + 메모 등
        Call<List<Voting_Title>> filed_voting_call1 = NetworkHelper.getInstance(base_url).getRetrofitService().GetVotingTitle();
        filed_voting_call1.enqueue(new Callback<List<Voting_Title>>() {
            @Override
            public void onResponse(Call<List<Voting_Title>> call, Response<List<Voting_Title>> response) {
                List<Voting_Title> filed_list = response.body();
                try {
                    if (response.isSuccessful() && !filed_list.get(0).getVotingbool().equals("")) {
                        Log.d("field_voting_title", filed_list.get(0).getVotingtitle());
                        field_title = findViewById(R.id.voting_field_title);
                        field_memo = findViewById(R.id.voting_field_memo);
                        field_day = findViewById(R.id.voting_field_day);
                        field_title.setText(filed_list.get(0).getVotingtitle());
                        field_memo.setText(filed_list.get(0).getVotingmemo());
                        field_day.setText(filed_list.get(0).getVotingday());
                    } else {
                        Log.d("field_voting_title", "실패");
                        field_title = findViewById(R.id.voting_field_title);
                        field_memo = findViewById(R.id.voting_field_memo);
                        field_day = findViewById(R.id.voting_field_day);
                        field_title.setText("");
                        field_memo.setText("");
                        field_day.setText("");
                    }
                }
                catch (Exception e)
                {
                    Log.d("field_voting_title", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<Voting_Title>> call, Throwable t) {
                Log.d("field_voting_title", "에러");
                field_title = findViewById(R.id.voting_field_title);
                field_memo = findViewById(R.id.voting_field_memo);
                field_day = findViewById(R.id.voting_field_day);
                field_title.setText("");
                field_memo.setText("");
                field_day.setText("");
            }
        });
        Call<List<Voting_User>> filed_voting_call3 = NetworkHelper.getInstance(base_url).getRetrofitService().GetVotingUser();
        filed_voting_call3.enqueue(new Callback<List<Voting_User>>() {
            @Override
            public void onResponse(Call<List<Voting_User>> call, Response<List<Voting_User>> response) {
                List<Voting_User> filed_list = response.body();
                try {
                    if (response.isSuccessful() && !filed_list.get(0).getVotinguser().equals("")) {
                        Log.d("field_voting_title", filed_list.get(0).getVotinguser());
                        field_user = findViewById(R.id.voting_field_user);
                        String tempuser = "투표자 : ";
                        for(int i=0; i<filed_list.size(); i++)
                        {
                            if(i ==0) {
                                tempuser += filed_list.get(i).getVotinguser();
                            }
                            else
                            {
                                tempuser += ", ";
                                tempuser += filed_list.get(i).getVotinguser();
                            }
                        }
                        field_user.setText(tempuser);
                    } else {
                        Log.d("field_voting_user", "실패");
                    }
                }
                catch (Exception e)
                {
                    Log.d("field_voting_user", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<Voting_User>> call, Throwable t) {
                Log.d("field_voting_user", "에러");
                field_user = findViewById(R.id.voting_field_user);
                field_user.setText("");

            }
        });
        // 현재 투표 내역 항목, 사람 수 등
        Call<List<Voting_Content>> filed_voting_call2 = NetworkHelper.getInstance(base_url).getRetrofitService().GetVotingContent();
        filed_voting_call2.enqueue(new Callback<List<Voting_Content>>() {
            @Override
            public void onResponse(Call<List<Voting_Content>> call, Response<List<Voting_Content>> response) {
                List<Voting_Content> filed_list = response.body();
                Collections.sort(filed_list,new ListComparator());
                try {
                    if (response.isSuccessful() && !filed_list.get(0).getVotingcon().equals("")) {
                        Log.d("field_voting_content", filed_list.get(0).getVotingcon());
                        field_content = findViewById(R.id.voting_field_content);
                        field_count = findViewById(R.id.voting_field_count);
                        field_allcount = findViewById(R.id.voting_field_allcount);
                        int cnt_all = 0;
                        String content_all_temp ="";
                        String count_temp ="";
                        for (int i = 0; i < filed_list.size(); i++) {
                            if(i ==0) {
                                cnt_all += filed_list.get(i).getVotingcontentcnt();
                                content_all_temp += filed_list.get(i).getVotingcon();
                                count_temp += filed_list.get(i).getVotingcontentcnt();
                            }
                            else
                            {
                                cnt_all += filed_list.get(i).getVotingcontentcnt();
                                content_all_temp += "\n";
                                content_all_temp += filed_list.get(i).getVotingcon();
                                count_temp += " 명\n";
                                count_temp += filed_list.get(i).getVotingcontentcnt();
                            }
                        }
                        field_content.setText(content_all_temp);
                        count_temp+= " 명";
                        field_count.setText(count_temp);
                        String countall_temp ="투표 인원 수 : "+String.valueOf(cnt_all) + "명";
                        field_allcount.setText(countall_temp);
                    } else {
                        Log.d("field_voting_content", "실패");
                        field_content = findViewById(R.id.voting_field_content);
                        field_count = findViewById(R.id.voting_field_count);
                        field_allcount = findViewById(R.id.voting_field_allcount);
                        field_content.setText("");
                        field_count.setText("");
                        field_allcount.setText("");
                    }
                }catch (Exception e)
                {

                }
            }

            @Override
            public void onFailure(Call<List<Voting_Content>> call, Throwable t) {
                Log.d("field_voting_content", "에러");
                field_content = findViewById(R.id.voting_field_content);
                field_count = findViewById(R.id.voting_field_count);
                field_allcount = findViewById(R.id.voting_field_allcount);
                field_content.setText("");
                field_count.setText("");
                field_allcount.setText("");
            }
        });
        // 과거 투표 내역
        String voting_td = (String) tb_main_id.getText();
        Call<List<Voting_All>> past_list_call = NetworkHelper.getInstance(base_url).getRetrofitService().GetVotingAllPrint(voting_td);
        past_list_call.enqueue(new Callback<List<Voting_All>>() {
            @Override
            public void onResponse(Call<List<Voting_All>> call, Response<List<Voting_All>> response) {
                List<Voting_All> vo_list = response.body();
                try {
                    if (response.isSuccessful() && !vo_list.get(0).getAlltitle().equals(null)) {
                        Log.d("과거 투표 내역", vo_list.get(0).getAlltitle());
                        VotingAdd(vo_list);
                    } else {
                        Log.d("과거 투표 내역", "실패");

                    }
                }
                catch (Exception e)
                {

                }
            }

            @Override
            public void onFailure(Call<List<Voting_All>> call, Throwable t) {
                Log.d("과거 투표 내역", "에러");
            }
        });
    }
    public void VotingAdd(List<Voting_All> voting_list)
    {
        recyclerView2 = findViewById(R.id.list_recycle_voting);
        recyclerView2.setLayoutManager(new LinearLayoutManager(this));
        recyclerView2.setAdapter(new RecyclerAdapter2(voting_list));
    }
    public void PastAdd(List<Past_All_Model> p_list)
    {
        recyclerView = findViewById(R.id.list_recycle);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.setAdapter(new RecyclerAdapter(p_list));
    }
    public void createNotificationChannel(String channelId, String channelName, int imprtance)
    {
        if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.O)
        {
            NotificationManager notificationManager = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
            notificationManager.createNotificationChannel(new NotificationChannel(channelId,channelName,imprtance));
        }
    }
    public void createNotification(String channelId, int id, String title, String text, Intent intent)
    {
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 0,intent, PendingIntent.FLAG_CANCEL_CURRENT);

        NotificationCompat.Builder builder = new NotificationCompat.Builder(this, channelId)
                .setPriority(NotificationCompat.PRIORITY_HIGH)
                .setSmallIcon(R.drawable.tdiconbackblue)
                .setContentTitle(title)
                .setContentText(text)
                .setAutoCancel(true) //클릭시 알림삭제
                .setContentIntent(pendingIntent)
                .setDefaults(Notification.DEFAULT_SOUND | Notification.DEFAULT_VIBRATE);
        NotificationManager notificationManager = (NotificationManager)getSystemService(NOTIFICATION_SERVICE);
        notificationManager.notify(id,builder.build());
    }

    @Override
    public void onClick(View view) {
        if(view.getId() == R.id.takeorder_end_btn)
        {
            //카운트 도달
            Call<List<String>> end_td_call = NetworkHelper.getInstance(base_url).getRetrofitService().GettakeorderEnd();
            end_td_call.enqueue(new Callback<List<String>>() {
                @Override
                public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                    List<String> filed_list = response.body();
                    try {
                        if (response.isSuccessful() && !filed_list.get(0).equals("")) {
                                takeorder_end_btn = findViewById(R.id.takeorder_end_btn);
                                takeorder_end_btn.setVisibility(View.INVISIBLE);
                                DataField();
                        } else {
                            Log.d("end_count", "실패");
                            takeorder_end_btn = findViewById(R.id.takeorder_end_btn);
                            takeorder_end_btn.setVisibility(View.VISIBLE);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

                @Override
                public void onFailure(Call<List<String>> call, Throwable t) {
                    Log.d("end_count", "에러");
                    try {
                        takeorder_end_btn = findViewById(R.id.takeorder_end_btn);
                        takeorder_end_btn.setVisibility(View.VISIBLE);
                    }
                    catch (Exception e)
                    {

                    }
                }
            });
        }
    }

    class MainRunnableThread implements Runnable{

        @Override
        public void run() {
            try {
                Thread.sleep(5000);
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
        }
    }
}

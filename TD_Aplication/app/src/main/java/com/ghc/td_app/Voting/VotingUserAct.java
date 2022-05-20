package com.ghc.td_app.Voting;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.RequiresApi;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.DisplayMetrics;
import android.util.Log;
import android.util.TypedValue;
import android.view.Gravity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

import com.ghc.td_app.ListComparator;
import com.ghc.td_app.MainActivity;
import com.ghc.td_app.Model.Ordering_public_Model;
import com.ghc.td_app.Model.Voting_Content;
import com.ghc.td_app.Model.Voting_Title;
import com.ghc.td_app.Model.Voting_User;
import com.ghc.td_app.NetworkHelper;
import com.ghc.td_app.R;
import com.ghc.td_app.TakeOrder.TakeOrderAct;

import java.text.Collator;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VotingUserAct  extends AppCompatActivity implements View.OnClickListener{
    public TextView user_title, user_day, user_memo, user_allcount, user_user;
    public LinearLayout voting_user_li;
    TextView vo_user_stor;
    String User_Id, Base_URL;
    int btn_id =0, id_temp;
    Context context;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.act_votinguser);
        context = this;
        Intent DataIntent = getIntent();
        User_Id = DataIntent.getStringExtra("ID");
        Base_URL = DataIntent.getStringExtra("URL");

        View back_view1;
        ImageView back_Image;
        TextView back_tv;
        back_view1 = findViewById(R.id.voting_user_view1);
        back_view1.setOnClickListener(this);
        back_Image = findViewById(R.id.voting_user_back_arrow);
        back_Image.setOnClickListener(this);
        back_tv = findViewById(R.id.voting_user_title);
        back_tv.setOnClickListener(this);

        voting_user_li = findViewById(R.id.voting_user_linear2);

        user_datafield();

        vo_user_stor = findViewById(R.id.voting_user_storage);
        vo_user_stor.setOnClickListener(this);

    }

    @SuppressLint({"ResourceAsColor", "ResourceType"})
    public void addradio(RadioGroup rdp, String str_radio, int id_str)
    {
        id_temp = 10000+id_str;
        RadioButton radioButton = new RadioButton(this);
        int height2 = (int)TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 11, getResources().getDisplayMetrics());
        RadioGroup.LayoutParams rp = new RadioGroup.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT);
        rp.bottomMargin = height2;
        radioButton.setId(id_temp);
        radioButton.setTextColor(getResources().getColor(R.drawable.radio_btn_color));
        radioButton.setTextAppearance(this,R.style.font_20_black);
        radioButton.setText(str_radio);
        radioButton.setLayoutParams(rp);
        radioButton.setOnClickListener(buttonClicked);
        rdp.addView(radioButton);

    }
    public View.OnClickListener buttonClicked = new View.OnClickListener() {
        @SuppressLint("ResourceAsColor")
        public void onClick(View v) {
            btn_id =  v.getId();
        }
    };

    public void addtextbox(String text_str, int id_str)
    {
        int box_temp_id = 20000 + id_str;
        TextView tv = new TextView(getApplicationContext());
        DisplayMetrics down_dm = getResources().getDisplayMetrics();
        int down_int = Math.round(6 * down_dm.density);
        DisplayMetrics up_dm = getResources().getDisplayMetrics();
        int up_int = Math.round(6 * down_dm.density);
        int height2 = (int)TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 10, getResources().getDisplayMetrics());
        LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
        p.bottomMargin = height2;
        tv.setPadding(0, up_int,0,down_int);
        tv.setText(text_str);
        tv.setLayoutParams(p);
        tv.setId(box_temp_id);
        tv.setTextAppearance(this,R.style.font_20_black);
        voting_user_li.addView(tv);
    }
    public void user_datafield()
    {

        // 현재 투표 내역 타이틀 + 메모 등
        Call<List<Voting_Title>> filed_voting_user_call1 = NetworkHelper.getInstance(Base_URL).getRetrofitService().GetVotingTitle();
        filed_voting_user_call1.enqueue(new Callback<List<Voting_Title>>() {
            @Override
            public void onResponse(Call<List<Voting_Title>> call, Response<List<Voting_Title>> response) {
                List<Voting_Title> filed_list = response.body();
                try {
                    if (response.isSuccessful() && !filed_list.get(0).getVotingtitle().equals("")) {
                        Log.d("field_user_title", filed_list.get(0).getVotingtitle());
                        user_title = findViewById(R.id.useract_title);
                        user_memo = findViewById(R.id.useract_memo);
                        user_day = findViewById(R.id.useract_day);
                        user_title.setText(filed_list.get(0).getVotingtitle());
                        user_memo.setText(filed_list.get(0).getVotingmemo());
                        user_day.setText(filed_list.get(0).getVotingday());
                    } else {
                        Log.d("field_user_title", "실패");
                        user_title = findViewById(R.id.voting_field_title);
                        user_memo = findViewById(R.id.voting_field_memo);
                        user_day = findViewById(R.id.voting_field_day);
                        user_title.setText("");
                        user_memo.setText("");
                        user_day.setText("");
                    }
                }
                catch (Exception e)
                {
                    Log.d("field_user_title", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<Voting_Title>> call, Throwable t) {
                Log.d("field_voting_title", "에러");
                user_title = findViewById(R.id.voting_field_title);
                user_memo = findViewById(R.id.voting_field_memo);
                user_day = findViewById(R.id.voting_field_day);
                user_title.setText("");
                user_memo.setText("");
                user_day.setText("");
            }
        });
        // 현재 투표 내역 항목
        Call<List<Voting_Content>> filed_voting_user_call2 = NetworkHelper.getInstance(Base_URL).getRetrofitService().GetVotingContent();
        filed_voting_user_call2.enqueue(new Callback<List<Voting_Content>>() {
            @Override
            public void onResponse(Call<List<Voting_Content>> call, Response<List<Voting_Content>> response) {
                List<Voting_Content> filed_list = response.body();

                Collections.sort(filed_list,new ListComparator());
                try {
                    if (response.isSuccessful() && !filed_list.get(0).getVotingcon().equals("")) {
                        Log.d("field_voting_content", filed_list.get(0).getVotingcon());
                        RadioGroup radioGroup;
                        radioGroup = findViewById(R.id.user_radio_group);
                        int all_cnt_user = 0;
                        for(int i=0; i<filed_list.size(); i++) {
                            addradio(radioGroup, filed_list.get(i).getVotingcon(), filed_list.get(i).getVotingnum());
                            addtextbox(String.valueOf(filed_list.get(i).getVotingcontentcnt())+ "명",  filed_list.get(i).getVotingnum());
                            all_cnt_user += filed_list.get(i).getVotingcontentcnt();
                        }
                        user_allcount = findViewById(R.id.user_voting_field_allcount);
                        user_allcount.setText("총 투표 인원 수 : "+String.valueOf(all_cnt_user) + "명");

                    } else {
                        Log.d("field_voting_content", "실패");
                        user_allcount = findViewById(R.id.user_voting_field_allcount);
                        user_allcount.setText("총 투표 인원 수 : ");
                    }
                }
                catch (Exception e)
                {
                    Log.d("field_voting_content", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<Voting_Content>> call, Throwable t) {
                Log.d("field_voting_content", "에러");
                user_allcount = findViewById(R.id.user_voting_field_allcount);
                user_allcount.setText("투표 인원 수 : ");
            }
        });
        // 현재 투표 내역 항목
        Call<List<Voting_User>> filed_voting_user_call3 = NetworkHelper.getInstance(Base_URL).getRetrofitService().GetVotingUser();
        filed_voting_user_call3.enqueue(new Callback<List<Voting_User>>() {
            @Override
            public void onResponse(Call<List<Voting_User>> call, Response<List<Voting_User>> response) {
                List<Voting_User> filed_list = response.body();
                try {
                    if (response.isSuccessful() && !filed_list.get(0).getVotinguser().equals("")) {
                        Log.d("field_voting_user", filed_list.get(0).getVotinguser());
                        String user_temp= "투표자 : ";
                        for(int i=0; i<filed_list.size();i++)
                        {
                            if(i ==0) {
                                user_temp += filed_list.get(i).getVotinguser();
                            }
                            else
                            {
                                user_temp += ", ";
                                user_temp += filed_list.get(i).getVotinguser();
                            }
                        }
                        user_user = findViewById(R.id.user_voting_field_user);
                        user_user.setText(user_temp);

                    } else {
                        Log.d("field_voting_user", "실패");
                        user_user = findViewById(R.id.user_voting_field_user);
                        user_user.setText("투표한 사람 : ");
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
                user_user = findViewById(R.id.user_voting_field_user);
                user_user.setText("투표한 사람 : ");
            }
        });
        Call<List<String>> check_voting = NetworkHelper.getInstance(Base_URL).getRetrofitService().GetVotingUserCheck(User_Id);
        check_voting.enqueue(new Callback<List<String>>() {
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> filed_list = response.body();
                try {
                    if (response.isSuccessful() && !filed_list.get(0).equals("")) {
                        Log.d("유저 체크", "성공 / 상태 : " + filed_list.get(0));
                        if (!filed_list.get(0).equals("f")) {
                            vo_user_stor.setText("수정");
                            int aa = 10000 + Integer.parseInt(filed_list.get(0));
                            String aaid = "" + aa;
                            int resID = getResources().getIdentifier(aaid, "id", getPackageName());
                            RadioButton radioButton = new RadioButton(getApplicationContext());
                            radioButton = findViewById(resID);
                            radioButton.performClick();
                        } else if (filed_list.get(0).equals("f")) {
                            vo_user_stor.setText("저장");
                        }
                    } else {
                        Log.d("유저 체크", "실패");
                        vo_user_stor.setText("저장");
                    }
                }
                catch (Exception e)
                {

                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("유저 체크", "오류" + t.getMessage());
                vo_user_stor.setText("저장");
            }
        });
    }
    @Override
    public void onClick(View view) {
        if(view.getId() == R.id.voting_user_back_arrow || view.getId() == R.id.voting_user_view1 || view.getId() == R.id.voting_user_title)
        {
            AlertDialog.Builder td_dlg = new AlertDialog.Builder(VotingUserAct.this, R.style.AlertDialog);
            td_dlg.setView(R.layout.voting_dlg_check);

            final AlertDialog alertDialog = td_dlg.create();
            alertDialog.show();
            ;

            TextView ok_btn = alertDialog.findViewById(R.id.dlg_takeorder_ok);
            ok_btn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Log.d("voting_back", "성공");
                    alertDialog.dismiss();
                    Intent intent = new Intent(VotingUserAct.this, MainActivity.class);
                    intent.putExtra("Check","1");
                    startActivity(intent);
                    overridePendingTransition(0, 0);
                    finish();

                }
            });

            TextView cancle_btn = alertDialog.findViewById(R.id.dlg_takeorder_close);
            cancle_btn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Log.d("voting_back", "성공");
                    alertDialog.dismiss();
                }
            });
        }
        if(view.getId() == R.id.voting_user_storage)
        {
            if(vo_user_stor.getText().equals("저장") && btn_id != 0) {
                int end_count = btn_id - 10000;
                Call<List<String>> votig_content_storage_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postVotingUser(
                        new Voting_User(end_count, User_Id));
                votig_content_storage_call.enqueue(new Callback<List<String>>() {
                    @Override
                    public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                        if (response.isSuccessful()) {
                            Log.d("유저 저장", "성공");
                            Intent intent = new Intent(VotingUserAct.this, MainActivity.class);
                            intent.putExtra("Check", "1");
                            startActivity(intent);
                            overridePendingTransition(0, 0);
                            finish();
                        } else {
                            Log.d("유저 저장", "실패");
                        }
                    }

                    @Override
                    public void onFailure(Call<List<String>> call, Throwable t) {
                        Log.d("유저 저장", "오류" + t.getMessage());
                    }
                });
            }
            else if (vo_user_stor.getText().equals("수정")&& btn_id != 0){
            int end_count = btn_id - 10000;
            Call<List<String>> votig_content_update_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postVotingUpdateUser(
                    new Voting_User(end_count, User_Id));
                votig_content_update_call.enqueue(new Callback<List<String>>() {
                @Override
                public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                    if (response.isSuccessful()) {
                        Log.d("유저 수정", "성공");
                        Intent intent = new Intent(VotingUserAct.this, MainActivity.class);
                        intent.putExtra("Check", "1");
                        startActivity(intent);
                        overridePendingTransition(0, 0);
                        finish();
                    } else {
                        Log.d("유저 수정", "실패");
                    }
                }

                @Override
                public void onFailure(Call<List<String>> call, Throwable t) {
                    Log.d("유저 수정", "오류" + t.getMessage());
                }
            });
            }
            else if (btn_id == 0)
            {
                Toast.makeText(getApplicationContext(), "선택을 하지 않았습니다.", Toast.LENGTH_SHORT).show();
            }
        }
    }
}

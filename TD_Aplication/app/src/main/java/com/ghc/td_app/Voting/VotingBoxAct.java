package com.ghc.td_app.Voting;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.InputFilter;
import android.util.Log;
import android.util.TypedValue;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.ghc.td_app.MainActivity;
import com.ghc.td_app.Model.Ordering_public_Model;
import com.ghc.td_app.Model.Voting_Content;
import com.ghc.td_app.Model.Voting_Title;
import com.ghc.td_app.NetworkHelper;
import com.ghc.td_app.R;
import com.ghc.td_app.TakeOrder.TakeOrderAct;
import com.ghc.td_app.TakeOrder.TakeOrder_Update_Model;

import org.w3c.dom.Text;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VotingBoxAct extends AppCompatActivity implements View.OnClickListener {
    public int v_count=0;
    public LinearLayout voting_li;
    public Button add_content_btn;
    String User_Id, Base_URL;
    public EditText voting_title, voting_memo;
    public EditText content_et,content_et1,content_et2;
    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.act_voting);


        Intent DataIntent = getIntent();
        User_Id = DataIntent.getStringExtra("ID");
        Base_URL = DataIntent.getStringExtra("URL");

        View back_view1;
        ImageView back_Image;
        TextView back_tv;
        back_view1 = findViewById(R.id.voting_view1);
        back_view1.setOnClickListener(this);
        back_Image = findViewById(R.id.voting_back_arrow);
        back_Image.setOnClickListener(this);
        back_tv = findViewById(R.id.voting_title);
        back_tv.setOnClickListener(this);

        TextView vo_stor;
        vo_stor = findViewById(R.id.voting_storage);
        vo_stor.setOnClickListener(this);

        voting_title = findViewById(R.id.voting_title_edit);
        voting_memo = findViewById(R.id.voting_memo_edit);
        voting_li = findViewById(R.id.voting_linear);

        addEdittext();

        add_content_btn =findViewById(R.id.voting_caregory_add);
        add_content_btn.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.M)
            @Override
            public void onClick(View view) {
                addEdittext();
            }
        });
    }

    @RequiresApi(api = Build.VERSION_CODES.M)
    public void addEdittext()
    {
        InputFilter[] filters = new InputFilter[]{
        new InputFilter.LengthFilter(15),
                new InputFilter.AllCaps()
        };
        content_et = new EditText(getApplicationContext());
        int height = (int)TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 50, getResources().getDisplayMetrics());
        int height2 = (int)TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 10, getResources().getDisplayMetrics());
        LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
        p.bottomMargin = height2;
        content_et.setLayoutParams(p);
        content_et.setWidth(ViewGroup.LayoutParams.MATCH_PARENT);
        content_et.setHeight(height);
        content_et.setFilters(filters);
        content_et.setId(v_count);
        content_et.setTextAppearance(R.style.font_15_primary);
        content_et.setBackgroundResource(R.drawable.roundbox_and_line);
        voting_li.addView(content_et);
        if(v_count != 0) {
            content_et.requestFocus();
        }
        v_count++;
    }
    @Override
    public void onClick(View view) {
        if(view.getId() == R.id.voting_back_arrow || view.getId() == R.id.voting_view1 || view.getId() == R.id.voting_title)
        {
            AlertDialog.Builder td_dlg = new AlertDialog.Builder(VotingBoxAct.this, R.style.AlertDialog);
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
                    Intent intent = new Intent(VotingBoxAct.this, MainActivity.class);
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
        if(view.getId() == R.id.voting_storage)
        {
            if(!voting_title.equals("") && v_count >= 2) {
                Date mdate = new Date(Calendar.getInstance().getTimeInMillis());
                SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy년 MM월 dd일 a hh시 mm분");
                String getTime = simpleDateFormat.format(mdate);
                boolean xtype = true;
                for(int a =0; a<v_count; a++)
                {
                    String editid = "" + a;
                    int resID = getResources().getIdentifier(editid, "id", getPackageName());
                    content_et1 = findViewById(resID);
                    String tempstr = content_et1.getText().toString();
                    for(int b=a; b<v_count; b++)
                    {
                        if(a != b)
                        {
                            String editid2 = "" + b;
                            int resID2 = getResources().getIdentifier(editid2, "id", getPackageName());
                            content_et2 = findViewById(resID2);
                            String tempstr2 = content_et2.getText().toString();
                            if(tempstr.equals(tempstr2))
                            {
                                xtype=false;
                            }
                        }
                    }
                }

                //타이틀입력
                if(xtype) {
                    Call<List<String>> votig_title_update_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postVotingTitle(
                            new Voting_Title("t", voting_title.getText().toString(), voting_memo.getText().toString(), getTime, "y"));
                    votig_title_update_call.enqueue(new Callback<List<String>>() {
                        @Override
                        public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                            if (response.isSuccessful()) {
                                //항목 입력
                                for (int j = 0; j < v_count; j++) {
                                    String editid = "" + j;
                                    int resID = getResources().getIdentifier(editid, "id", getPackageName());
                                    content_et = findViewById(resID);
                                    String tempstr = content_et.getText().toString();
                                    if (!tempstr.equals("")) {
                                        Log.d("항목 확인 : ", resID + "//" + "항목 번호" + j + "항목명 : " + tempstr);
                                        Call<List<String>> votig_content_update_call = NetworkHelper.getInstance(Base_URL).getRetrofitService().postVotingContent(
                                                new Voting_Content(j, 0, tempstr));
                                        votig_content_update_call.enqueue(new Callback<List<String>>() {
                                            @Override
                                            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                                                if (response.isSuccessful()) {
                                                } else {
                                                    Log.d("항목 저장", "실패");
                                                }
                                            }

                                            @Override
                                            public void onFailure(Call<List<String>> call, Throwable t) {
                                                Log.d("항목 저장", "오류" + t.getMessage());
                                            }
                                        });
                                    }
                                }
                                // main 화면 전환
                                Log.d("항목 저장", "성공");
                                Intent intent = new Intent(VotingBoxAct.this, MainActivity.class);
                                intent.putExtra("Check", "1");
                                startActivity(intent);
                                overridePendingTransition(0, 0);
                                finish();
                            } else {
                                Log.d("타이틀 저장", "실패");
                            }
                        }

                        @Override
                        public void onFailure(Call<List<String>> call, Throwable t) {
                            Log.d("타이틀 저장", "오류" + t.getMessage());
                        }
                    });
                }
                else
                {
                    Toast.makeText(getApplicationContext(), "중복된 항목이 있습니다.", Toast.LENGTH_SHORT).show();
                }
            }
            else
            {
                Toast.makeText(getApplicationContext(), "타이틀 입력과 항목 2개이상이 필요합니다.", Toast.LENGTH_SHORT).show();
            }
        }
    }
}


package com.ghc.td_app.Setting;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.ghc.td_app.MainActivity;
import com.ghc.td_app.NetworkHelper;
import com.ghc.td_app.R;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SettingAct extends AppCompatActivity implements View.OnClickListener{

    public String base_url=null;
    public String base_name=null;
    public  EditText Setting_et_name;
    public  EditText Setting_et_ip;
    public  Button Setting_idbtn;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.act_setting);

        Setting_idbtn = findViewById(R.id.setting_idcheck_btn);
        try {
            FileInputStream main_name_fis = openFileInput("settingname.dat");
            DataInputStream main_name_dis = new DataInputStream(main_name_fis);
            String main_name_data = main_name_dis.readUTF();
            base_name=main_name_data;
            if(base_name != null)
            {
                Setting_idbtn.setText("ID변경");
            }
            main_name_dis.close();

        } catch (FileNotFoundException e) {

        } catch (IOException e) {

            e.printStackTrace();

        }
        try {
            FileInputStream main_ip_fis = openFileInput("settingip.dat");
            DataInputStream main_ip_dis = new DataInputStream(main_ip_fis);
            String main_ip_data = main_ip_dis.readUTF();
            base_url = main_ip_data;
            main_ip_dis.close();

        } catch (FileNotFoundException e) {

        } catch (IOException e) {

            e.printStackTrace();
        }
        Setting_idbtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                if(Setting_idbtn.getText() == "중복확인")
                {
                    fun_idcheck(Setting_et_name.getText().toString());
                }
                else{
                    Setting_et_name.setEnabled(true);
                    Setting_et_name.setFocusable(true);
                    Setting_idbtn.setText("중복확인");
                    Setting_et_name.requestFocus();
                }
            }
        });
        Setting_et_name = findViewById(R.id.setting_name_edit);
        Setting_et_name.setText(base_name);
        if(TextUtils.isEmpty(Setting_et_name.getText()))
        {
            Toast.makeText(getApplicationContext(), "사용할 ID를 입력해주세요.", Toast.LENGTH_SHORT).show();
            Setting_et_name.setEnabled(true);
            Setting_et_name.requestFocus();
            Setting_idbtn.setText("중복확인");
        }

        Setting_et_name.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View view, int i, KeyEvent keyEvent) {
                if(i == keyEvent.KEYCODE_ENTER)
                    return true;
                return false;
            }
        });
        Setting_et_ip = findViewById(R.id.setting_ip_edit);
        Setting_et_ip.setText(base_url);
        Setting_et_ip.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            @Override
            public void onFocusChange(View view, boolean b) {
                if(b)
                {

                }
                else {
                    InputMethodManager immhide = (InputMethodManager) getSystemService(Activity.INPUT_METHOD_SERVICE);
                    immhide.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);

                }
            }
        });
        Setting_et_ip.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View view, int i, KeyEvent keyEvent) {
                if(i == keyEvent.KEYCODE_ENTER)
                    return true;
                return false;
            }
        });
        View back_view;
        ImageView back_Image;
        TextView back_tv;
        back_view = findViewById(R.id.setting_view);
        back_view.setOnClickListener(this);
        back_Image = findViewById(R.id.setting_back_arrow);
        back_Image.setOnClickListener(this);
        back_tv = findViewById(R.id.setting_title);
        back_tv.setOnClickListener(this);

    }
    private void fun_idcheck(final String check_id){
        Call<List<String>> list_check_call = NetworkHelper.getInstance(base_url).getRetrofitService().getUserId(check_id);
        list_check_call.enqueue(new Callback<List<String>>() {
            @Override
            public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                List<String> stm = response.body();
                if (response.isSuccessful()) {

                    Log.d("idcheck", "ID : " + stm.get(0).toString());
                    Log.d("idcheck", "ID2 : " + check_id.trim());
                    if(check_id.trim().equals(stm.get(0).trim()))
                    {
                        InputMethodManager immhide = (InputMethodManager) getSystemService(Activity.INPUT_METHOD_SERVICE);
                        immhide.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                        Toast.makeText(getApplicationContext(), "사용 가능한 ID입니다.", Toast.LENGTH_SHORT).show();
                        Setting_et_name.setFocusable(false);
                        Setting_idbtn.setText("ID변경");
                        Setting_et_name.setEnabled(false);
                    }
                    else if(!check_id.trim().equals(stm.get(0).trim()))
                    {
                        InputMethodManager immhide = (InputMethodManager) getSystemService(Activity.INPUT_METHOD_SERVICE);
                        immhide.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                        Toast.makeText(getApplicationContext(), "중복 ID입니다. 다른 ID를 입력해주세요.", Toast.LENGTH_SHORT).show();
                        Setting_et_name.setFocusable(true);
                        Setting_idbtn.setText("중복확인");
                    }
                }
                else {
                    Log.d("idcheck", "실패");
                }
            }

            @Override
            public void onFailure(Call<List<String>> call, Throwable t) {
                Log.d("idcheck", "에러");
            }
        });
    }

    @Override
    public void onClick(View view) {
        if(TextUtils.isEmpty(Setting_et_name.getText()) && !TextUtils.isEmpty(Setting_et_ip.getText()))
        {
            Toast.makeText(getApplicationContext(), "사용할 ID를 입력해주세요.", Toast.LENGTH_SHORT).show();
            Setting_et_name.requestFocus();
        }
        if(!TextUtils.isEmpty(Setting_et_name.getText()) && TextUtils.isEmpty(Setting_et_ip.getText()))
        {
            Toast.makeText(getApplicationContext(), "서버 IP를 입력해주세요.", Toast.LENGTH_SHORT).show();
            Setting_et_ip.requestFocus();
        }
        if(TextUtils.isEmpty(Setting_et_name.getText()) && TextUtils.isEmpty(Setting_et_ip.getText()))
        {
            Toast.makeText(getApplicationContext(), "빈 설정 부분을 입력해주세요.", Toast.LENGTH_SHORT).show();
            Setting_et_name.requestFocus();
        }
        if(!TextUtils.isEmpty(Setting_et_name.getText()) && !TextUtils.isEmpty(Setting_et_ip.getText()) && TextUtils.isEmpty(base_url))
        {
            try {
                FileOutputStream setting_name_fos = openFileOutput("settingname.dat",MODE_APPEND);
                DataOutputStream setting_name_dos = new DataOutputStream(setting_name_fos);
                setting_name_dos.writeUTF(Setting_et_name.getText().toString());
                setting_name_dos.flush();
                setting_name_dos.close();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            try {
                FileOutputStream setting_ip_fos = openFileOutput("settingip.dat",MODE_APPEND);
                DataOutputStream setting_ip_dos = new DataOutputStream(setting_ip_fos);
                setting_ip_dos.writeUTF("http://"+Setting_et_ip.getText().toString()+"/");
                setting_ip_dos.flush();
                setting_ip_dos.close();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }

            Intent intent = new Intent(SettingAct.this, MainActivity.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
        }
        if(!TextUtils.isEmpty(Setting_et_name.getText()) && !TextUtils.isEmpty(Setting_et_ip.getText()) && !TextUtils.isEmpty(base_url))
        {
            try {
                FileOutputStream setting_name_fos = openFileOutput("settingname.dat",MODE_PRIVATE);
                DataOutputStream setting_name_dos = new DataOutputStream(setting_name_fos);
                setting_name_dos.writeUTF(Setting_et_name.getText().toString());
                setting_name_dos.flush();
                setting_name_dos.close();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            try {
                FileOutputStream setting_ip_fos = openFileOutput("settingip.dat",MODE_PRIVATE);
                DataOutputStream setting_ip_dos = new DataOutputStream(setting_ip_fos);
                setting_ip_dos.writeUTF(Setting_et_ip.getText().toString());
                setting_ip_dos.flush();
                setting_ip_dos.close();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }

            Intent intent = new Intent(SettingAct.this, MainActivity.class);
            startActivity(intent);
            overridePendingTransition(0, 0);
            finish();
        }
    }
}

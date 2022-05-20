package com.ghc.td_app;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class NetworkHelper {
    public Retrofit retrofit;
    private RetrofitService retrofitService;
    public static  NetworkHelper networkHelper;

    public NetworkHelper(String helperurl){
        OkHttpClient.Builder okHttpClient = new OkHttpClient.Builder();
        HttpLoggingInterceptor loggingInterceptor = new HttpLoggingInterceptor();
        loggingInterceptor.setLevel(HttpLoggingInterceptor.Level.BODY);
        okHttpClient.addInterceptor(loggingInterceptor);

         retrofit = new Retrofit.Builder()
                .baseUrl(helperurl)
                .addConverterFactory(GsonConverterFactory.create())
                 .client(okHttpClient.build())
                .build();
        retrofitService = retrofit.create(RetrofitService.class);
    }

    public static synchronized NetworkHelper getInstance(String url){
        if(null==networkHelper)
        {
            Log.d("helper",url);
            networkHelper = new NetworkHelper(url);
        }
        return networkHelper;
    }
    public RetrofitService getRetrofitService() {
        return retrofitService;
    }
}
  /*OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .connectTimeout(1, TimeUnit.MINUTES)
                .readTimeout(30,TimeUnit.SECONDS)
                .writeTimeout(15,TimeUnit.SECONDS)
                .build();*/
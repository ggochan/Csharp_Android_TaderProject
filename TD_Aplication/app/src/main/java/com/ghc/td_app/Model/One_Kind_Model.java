package com.ghc.td_app.Model;

import android.arch.lifecycle.LiveData;
import android.arch.lifecycle.MutableLiveData;
import android.arch.lifecycle.ViewModel;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.List;

public class One_Kind_Model extends ViewModel {
    @SerializedName("comboKind")
    private String comboKind;

    public String getComboKind() {
        return comboKind;
    }

    public void setComboKind(String comboKind) {
        this.comboKind = comboKind;
    }
    public One_Kind_Model(){}
    public One_Kind_Model(One_Kind_Model km) {
        this.comboKind=km.getComboKind();
    }
    @Override
    public String toString() {
        return comboKind;
    }
}

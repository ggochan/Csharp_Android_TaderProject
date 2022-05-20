package com.ghc.td_app.Model;

import com.google.gson.annotations.SerializedName;

public class One_Store_Model {
    @SerializedName("comboStore")
    private String comboStore;

    public String getComboStore() {
        return comboStore;
    }

    public void setComboStore(String comboStore) {
        this.comboStore = comboStore;
    }

    public One_Store_Model(String comboStore) {
        this.comboStore=comboStore;
    }
    @Override
    public String toString() {
        return comboStore;
    }
}

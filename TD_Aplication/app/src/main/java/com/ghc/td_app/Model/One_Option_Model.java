package com.ghc.td_app.Model;

import com.google.gson.annotations.SerializedName;

public class One_Option_Model {
    @SerializedName("optionCombo")
    private String comboOption;

    public String getComboOption() {
        return comboOption;
    }

    public void setComboOption(String comboOption) {
        this.comboOption = comboOption;
    }

    public One_Option_Model(String comboOption) {
        this.comboOption=comboOption;
    }
    @Override
    public String toString() {
        return comboOption;
    }
}

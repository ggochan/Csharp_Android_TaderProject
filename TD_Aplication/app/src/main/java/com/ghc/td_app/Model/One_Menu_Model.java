package com.ghc.td_app.Model;

import com.google.gson.annotations.SerializedName;

public class One_Menu_Model {
    @SerializedName("menuCombo")
    private String comboMenu;

    public String getComboMenu() {
        return comboMenu;
    }

    public void setComboMenu(String comboMenu) {
        this.comboMenu = comboMenu;
    }

    public One_Menu_Model(String comboMenu) {
        this.comboMenu=comboMenu;
    }
    @Override
    public String toString() {
        return comboMenu;
    }
}

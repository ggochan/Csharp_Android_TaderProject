package com.ghc.td_app.Model;

import com.google.gson.annotations.SerializedName;

public class Ordering_public_Model {

    @SerializedName("UserName")
    private String UserName;

    @SerializedName("MenuName")
    private String MenuName;

    @SerializedName("OptionDes")
    private String OptionDes;

    public Ordering_public_Model(String UserName, String MenuName, String OptionDes) {
        this.UserName=UserName;
        this.MenuName=MenuName;
        this.OptionDes=OptionDes;
    }

    public String getUserName() {
        return UserName;
    }

    public void setUserName(String userName) {
        UserName = userName;
    }

    public String getMenuName() {
        return MenuName;
    }

    public void setMenuName(String menuName) {
        MenuName = menuName;
    }

    public String getOptionDes() {
        return OptionDes;
    }

    public void setOptionDes(String optionDes) {
        OptionDes = optionDes;
    }

}

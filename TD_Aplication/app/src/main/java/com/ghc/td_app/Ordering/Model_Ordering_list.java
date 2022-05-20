package com.ghc.td_app.Ordering;

import com.google.gson.annotations.SerializedName;

public class Model_Ordering_list {
    @SerializedName("menu")
    private String menu;

    @SerializedName("menu_Count")
    private int menu_Count;

    public Model_Ordering_list(String menu, int menu_Count) {
        this.menu=menu;
        this.menu_Count=menu_Count;
    }
    public String getMenu() {
        return menu;
    }

    public void setMenu(String menu) {
        this.menu = menu;
    }

    public int getMenu_Count() {
        return menu_Count;
    }

    public void setMenu_Count(int menu_Count) {
        this.menu_Count = menu_Count;
    }

}

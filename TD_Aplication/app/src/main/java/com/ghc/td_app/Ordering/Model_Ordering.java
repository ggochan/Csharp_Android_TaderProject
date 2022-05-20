package com.ghc.td_app.Ordering;

public class Model_Ordering {

    private String num;
    private String menu;
    private String menu_count;

    public Model_Ordering(String num, String menu, String menu_count) {
        this.num = num;
        this.menu = menu;
        this.menu_count = menu_count;
    }

    public String getNum() {
        return num;
    }

    public void setNum(String num) {
        this.num = num;
    }

    public String getMenu() {
        return menu;
    }

    public void setMenu(String menu) {
        this.menu = menu;
    }

    public String getMenu_count() {
        return menu_count;
    }

    public void setMenu_count(String menu_count) {
        this.menu_count = menu_count;
    }

}

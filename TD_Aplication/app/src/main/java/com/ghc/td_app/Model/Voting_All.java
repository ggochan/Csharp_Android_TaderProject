package com.ghc.td_app.Model;
import com.google.gson.annotations.SerializedName;

public class Voting_All {

    @SerializedName("alltitle")
    private String Alltitle;

    @SerializedName("allmemo")
    private String Allmemo;

    @SerializedName("allday")
    private String Allday;

    @SerializedName("allyn")
    private String Allyn;

    @SerializedName("allcontent")
    private String Allcontent;

    @SerializedName("allcount")
    private String Allcount;

    @SerializedName("alluser")
    private String Alluser;

    @SerializedName("allcountall")
    private int AllcountAll;

    public Voting_All(String Alltitle, String Allmemo, String Allday, String Allyn, String Allcontent, String Allcount, String Alluser, int AllcountAll) {
        this.Alltitle=Alltitle;
        this.Allmemo=Allmemo;
        this.Allday=Allday;
        this.Allyn=Allyn;
        this.Allcontent=Allcontent;
        this.Allcount=Allcount;
        this.Alluser=Alluser;
        this.AllcountAll=AllcountAll;
    }

    public String getAlltitle() {
        return Alltitle;
    }

    public void setAlltitle(String alltitle) {
        Alltitle = alltitle;
    }

    public String getAllmemo() {
        return Allmemo;
    }

    public void setAllmemo(String allmemo) {
        Allmemo = allmemo;
    }

    public String getAllday() {
        return Allday;
    }

    public void setAllday(String allday) {
        Allday = allday;
    }

    public String getAllyn() {
        return Allyn;
    }

    public void setAllyn(String allyn) {
        Allyn = allyn;
    }

    public String getAllcontent() {
        return Allcontent;
    }

    public void setAllcontent(String allcontent) {
        Allcontent = allcontent;
    }

    public String getAllcount() {
        return Allcount;
    }

    public void setAllcount(String allcount) {
        Allcount = allcount;
    }

    public String getAlluser() {
        return Alluser;
    }

    public void setAlluser(String alluser) {
        Alluser = alluser;
    }

    public int getAllcountAll() {
        return AllcountAll;
    }

    public void setAllcountAll(int allcountAll) {
        AllcountAll = allcountAll;
    }


}

package com.ghc.td_app.Model;
import com.google.gson.annotations.SerializedName;

public class Voting_Title {
    @SerializedName("votingbool")
    private String Votingbool;

    @SerializedName("votingtitle")
    private String Votingtitle;

    @SerializedName("votingmemo")
    private String Votingmemo;

    @SerializedName("votingday")
    private String Votingday;

    @SerializedName("votingyn")
    private String Votingyn;

    public Voting_Title(String Votingbool, String Votingtitle, String Votingmemo, String Votingday, String Votingyn) {
        this.Votingbool=Votingbool;
        this.Votingtitle=Votingtitle;
        this.Votingmemo=Votingmemo;
        this.Votingday=Votingday;
        this.Votingyn=Votingyn;
    }

    public String getVotingbool() {
        return Votingbool;
    }

    public void setVotingbool(String votingbool) {
        Votingbool = votingbool;
    }

    public String getVotingtitle() {
        return Votingtitle;
    }

    public void setVotingtitle(String votingtitle) {
        Votingtitle = votingtitle;
    }

    public String getVotingmemo() {
        return Votingmemo;
    }

    public void setVotingmemo(String votingmemo) {
        Votingmemo = votingmemo;
    }

    public String getVotingday() {
        return Votingday;
    }

    public void setVotingday(String votingday) {
        Votingday = votingday;
    }

    public String getVotingyn() {
        return Votingyn;
    }

    public void setVotingyn(String votingyn) {
        Votingyn = votingyn;
    }
}

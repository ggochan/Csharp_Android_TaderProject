package com.ghc.td_app.Model;
import com.google.gson.annotations.SerializedName;

public class Voting_Content {
    @SerializedName("votingnum")
    private int Votingnum;

    @SerializedName("votingcontentcnt")
    private int Votingcontentcnt;

    @SerializedName("votingcon")
    private String Votingcon;

    public Voting_Content(int Votingnum, int Votingcontentcnt, String Votingcon) {
        this.Votingnum=Votingnum;
        this.Votingcontentcnt=Votingcontentcnt;
        this.Votingcon=Votingcon;
    }

    public int getVotingnum() {
        return Votingnum;
    }

    public void setVotingnum(int votingnum) {
        Votingnum = votingnum;
    }

    public int getVotingcontentcnt() {
        return Votingcontentcnt;
    }

    public void setVotingcontentcnt(int votingcontentcnt) {
        Votingcontentcnt = votingcontentcnt;
    }

    public String getVotingcon() {
        return Votingcon;
    }

    public void setVotingcon(String votingcon) {
        Votingcon = votingcon;
    }
}

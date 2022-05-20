package com.ghc.td_app.Model;
import com.google.gson.annotations.SerializedName;

public class Voting_User {
    @SerializedName("votingnum")
    private int Votingnum;

    @SerializedName("votinguser")
    private String Votinguser;

    public Voting_User(int Votingnum, String Votinguser) {
        this.Votingnum=Votingnum;
        this.Votinguser=Votinguser;
    }

    public int getVotingnum() {
        return Votingnum;
    }

    public void setVotingnum(int votingnum) {
        Votingnum = votingnum;
    }

    public String getVotinguser() {
        return Votinguser;
    }

    public void setVotinguser(String votinguser) {
        Votinguser = votinguser;
    }
}

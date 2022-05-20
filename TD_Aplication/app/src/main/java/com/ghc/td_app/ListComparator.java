package com.ghc.td_app;

import com.ghc.td_app.Model.Voting_Content;

import java.util.Comparator;

public class ListComparator implements Comparator<Voting_Content> {

    @Override
    public int compare(Voting_Content o1, Voting_Content o2) {
        int testint1 = o1.getVotingnum();
        int testint2 = o2.getVotingnum();

        if(testint1 > testint2){
            return 1;
        }else if(testint1 < testint2){
            return -1;
        }else{
            return 0;
        }

    }
}

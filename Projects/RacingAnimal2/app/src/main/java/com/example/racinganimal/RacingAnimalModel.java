package com.example.racinganimal;

import android.os.CountDownTimer;

import java.util.ArrayList;
import java.util.Random;

public class RacingAnimalModel {

    private ArrayList<Integer> listRanking;
    private ArrayList<Integer> scoreArray;
    private Random random;
    private int maxStep ;
    private int period;
    private int interval;
    private int bet;

    /*
    constructor
     */
    public RacingAnimalModel(int maxStep,int period, int interval){
        this.period = period;
        this.interval = interval;
        this.maxStep = maxStep;
        listRanking = new ArrayList<Integer>();
        scoreArray = new ArrayList<>();
        scoreArray.add(2);
        scoreArray.add(6);
        scoreArray.add(8);
        random = new Random();
    }

    /*
    getter for MaxStep
     */
    public int getMaxStep(){
        return maxStep;
    }

    /*
    getter for Step
     */
    public int getStep(){
        return random.nextInt(maxStep);
    }

    public void addId(int id){
        if (!listRanking.contains(id)){
            listRanking.add(id);
        }
    }

    public int getIdAtPosition(int i){
        return listRanking.get(i);
    }

    /*
    getter for Position
     */
    public int getPosition(int id){
        return listRanking.indexOf(id);
    }

    /*
    clear the ranking box
     */
    public void clearRanking(){
        listRanking.clear();
    }

    /*
    getter for the period of the game
     */
    public int getPeriod(){
        return period;
    }

    /*
    getter for the interval
     */
    public int getInterval(){
        return interval;
    }

    /*
    getter for the array of bet score for adapter
     */
    public ArrayList<Integer> getScoreArray(){
        return scoreArray;
    }

    public void setBet(int bet){
        this.bet = bet;
    }

    /*
    getter for bet score
     */
    public int getBet(){
        return bet;
    }

    public boolean isListRankingEmpty(){
        if(listRanking == null){
            return true;
        } else {
            return false;
        }
    }
}

package com.example.racinganimal;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Adapter;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.ImageButton;
import android.widget.ProgressBar;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.SeekBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.EventListener;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.FirebaseFirestoreException;

import org.w3c.dom.Document;
import org.w3c.dom.Text;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Objects;
import java.util.Random;

import javax.annotation.Nullable;

public class MainActivity extends AppCompatActivity {

    TextView txtGrade, firstPosition, secondPosition, thirdPosition;
    CheckBox cb1, cb2, cb3;
    SeekBar sk1, sk2, sk3;
    ImageButton btnPlay, btnClose;
    RelativeLayout rnkBox;
    RacingAnimalModel model;
    Spinner scoreSpinner;
    ArrayList<Integer> scoreArray;
    ArrayList<Integer> previousState;
    Button boostButton, rankingBtn;
    final String PREVIOUS_STATE = "previous state";
    FirebaseAuth firebaseAuth;
    FirebaseFirestore firestore;
    String userID;
    long currScore;
    DocumentReference documentReference;

    @RequiresApi(api = Build.VERSION_CODES.KITKAT)
    @Override
    protected void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        project();
        disenableSeekBar();
        disableBoostButton();
        rnkBox.setVisibility(View.INVISIBLE);
        firebaseAuth = FirebaseAuth.getInstance();
        firestore = FirebaseFirestore.getInstance();
        userID = firebaseAuth.getCurrentUser().getUid();
        documentReference = firestore.collection("users").document(userID);

        /*
        This is used to show the updated grade in the tdtGrade textview after each game
         */
        documentReference.addSnapshotListener(this, new EventListener<DocumentSnapshot>() {
            @Override
            public void onEvent(@Nullable DocumentSnapshot documentSnapshot, @Nullable FirebaseFirestoreException e) {
                txtGrade.setText(documentSnapshot.getLong("userScore")+"");
                Log.d("Tag", "update txt Grade textView");

            }
        });

        /*
        This code is from Firebase and is used to capture and notify any changes in user's data.
         */
        documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            @Override
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                if (task.isSuccessful()) {
                    DocumentSnapshot document = task.getResult();
                    if (document.exists()) {
                        Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                        currScore = document.getLong("userScore");
                        Log.d("TAG", "The score is inside : " + currScore);
                    } else {
                        Log.d("TAG", "No such document");
                    }
                } else {
                    Log.d("TAG", "get failed with ", task.getException());
                }
            }
        });

        /*
        create a game model with 3 parameters:
        _ maxStep: the maximum step for each each clock tick
        _ period: the maximum period for each game
        _ interval: the period for each clock tick
         */
        model = new RacingAnimalModel(5,60000,300);
        scoreArray = model.getScoreArray();

        /*
        Score spinner for user to pick how many score they want to each race
         */
        final ArrayAdapter adapter = new ArrayAdapter(MainActivity.this,R.layout.spinner_item,scoreArray);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        scoreSpinner.setAdapter(adapter);
        scoreSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                int bet = (Integer) adapterView.getItemAtPosition(i);
                model.setBet(bet);
                Toast.makeText(MainActivity.this, bet +"", Toast.LENGTH_SHORT).show();
            }
            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        /*
        Creating a count down timer to run the game
        Every clock tick, it will set each animal to the new position base on a random amount of steps generated by game model
        The count down timer terminates when all three animal arrive the end and open the ranking up to show the first place, secone place and third place
         */
        final CountDownTimer countDownTimer = new CountDownTimer(model.getPeriod(),model.getInterval()) {
            @Override
            public void onTick(long l) {
                int one = model.getStep();
                int two = model.getStep();
                int three = model.getStep();
                if(sk1.getProgress() >= sk1.getMax() && sk2.getProgress() >= sk2.getMax() && sk3.getProgress() >= sk3.getMax()){
                    this.cancel();
                    ranking();
                    rnkBox.setVisibility(View.VISIBLE);
                    if(cb1.isChecked()){
//                        model.calculateScore(sk1.getId());
                        if(model.getPosition(sk1.getId()) == 0){
                            currScore += model.getBet();
                            documentReference.update("userScore",currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                                @Override
                                public void onSuccess(Void aVoid) {
                                    documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                        @Override
                                        public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                            if (task.isSuccessful()) {
                                                DocumentSnapshot document = task.getResult();
                                                if (document.exists()) {
                                                    Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                    txtGrade.setText(document.getLong("userScore")+"");
                                                } else {
                                                    Log.d("TAG", "No such document");
                                                }
                                            } else {
                                                Log.d("TAG", "get failed with ", task.getException());
                                            }
                                        }
                                    });
                                    Log.d("Tag", "DocumentSnapshot successfully updated!");
                                }
                            })
                                    .addOnFailureListener(new OnFailureListener() {
                                        @Override
                                        public void onFailure(@NonNull Exception e) {
                                            Log.w("Tag", "Error updating document", e);
                                        }
                                    });
                            Log.d("Tag","currentScore :" + currScore);

                        } else {
                            currScore -= model.getBet();
                            documentReference.update("userScore",currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                                @Override
                                public void onSuccess(Void aVoid) {

                                    documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                        @Override
                                        public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                            if (task.isSuccessful()) {
                                                DocumentSnapshot document = task.getResult();
                                                if (document.exists()) {
                                                    Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                    txtGrade.setText(document.getLong("userScore")+"");
                                                } else {
                                                    Log.d("TAG", "No such document");
                                                }
                                            } else {
                                                Log.d("TAG", "get failed with ", task.getException());
                                            }
                                        }
                                    });


                                    Log.d("Tag", "DocumentSnapshot successfully updated!");
                                }
                            })
                                    .addOnFailureListener(new OnFailureListener() {
                                        @Override
                                        public void onFailure(@NonNull Exception e) {
                                            Log.w("Tag", "Error updating document", e);
                                        }
                                    });
                            Log.d("Tag","currentScore :" + currScore);
                        }
                    }
                    if(cb2.isChecked()){
                        if(model.getPosition(sk2.getId()) == 0){
                            currScore += model.getBet();
                            documentReference.update("userScore",currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                                @Override
                                public void onSuccess(Void aVoid) {

                                    documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                        @Override
                                        public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                            if (task.isSuccessful()) {
                                                DocumentSnapshot document = task.getResult();
                                                if (document.exists()) {
                                                    Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                    txtGrade.setText(document.getLong("userScore")+"");
                                                } else {
                                                    Log.d("TAG", "No such document");
                                                }
                                            } else {
                                                Log.d("TAG", "get failed with ", task.getException());
                                            }
                                        }
                                    });

                                    Log.d("Tag", "DocumentSnapshot successfully updated!");
                                }
                            })
                                    .addOnFailureListener(new OnFailureListener() {
                                        @Override
                                        public void onFailure(@NonNull Exception e) {
                                            Log.w("Tag", "Error updating document", e);
                                        }
                                    });
                            Log.d("Tag","currentScore :" + currScore);

                        } else {
                            currScore -= model.getBet();
                            documentReference.update("userScore",currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                                @Override
                                public void onSuccess(Void aVoid) {

                                    documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                        @Override
                                        public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                            if (task.isSuccessful()) {
                                                DocumentSnapshot document = task.getResult();
                                                if (document.exists()) {
                                                    Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                    txtGrade.setText(document.getLong("userScore")+"");
                                                } else {
                                                    Log.d("TAG", "No such document");
                                                }
                                            } else {
                                                Log.d("TAG", "get failed with ", task.getException());
                                            }
                                        }
                                    });

                                    Log.d("Tag", "DocumentSnapshot successfully updated!");
                                }
                            })
                                    .addOnFailureListener(new OnFailureListener() {
                                        @Override
                                        public void onFailure(@NonNull Exception e) {
                                            Log.w("Tag", "Error updating document", e);
                                        }
                                    });
                            Log.d("Tag","currentScore :" + currScore);
                        }
                    }
                    if(cb3.isChecked()){
//                        model.calculateScore(sk3.getId());
                        if(model.getPosition(sk3.getId()) == 0){
                            currScore += model.getBet();
                            documentReference.update("userScore",currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                                @Override
                                public void onSuccess(Void aVoid) {
                                    documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                        @Override
                                        public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                            if (task.isSuccessful()) {
                                                DocumentSnapshot document = task.getResult();
                                                if (document.exists()) {
                                                    Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                    txtGrade.setText(document.getLong("userScore")+"");
                                                } else {
                                                    Log.d("TAG", "No such document");
                                                }
                                            } else {
                                                Log.d("TAG", "get failed with ", task.getException());
                                            }
                                        }
                                    });

                                    Log.d("Tag", "DocumentSnapshot successfully updated!");
                                }
                            })
                                    .addOnFailureListener(new OnFailureListener() {
                                        @Override
                                        public void onFailure(@NonNull Exception e) {
                                            Log.w("Tag", "Error updating document", e);
                                        }
                                    });
                            Log.d("Tag","currentScore :" + currScore);

                        } else {
                            currScore -= model.getBet();
                            documentReference.update("userScore",currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                                @Override
                                public void onSuccess(Void aVoid) {
                                    documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                        @Override
                                        public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                            if (task.isSuccessful()) {
                                                DocumentSnapshot document = task.getResult();
                                                if (document.exists()) {
                                                    Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                    txtGrade.setText(document.getLong("userScore")+"");
                                                } else {
                                                    Log.d("TAG", "No such document");
                                                }
                                            } else {
                                                Log.d("TAG", "get failed with ", task.getException());
                                            }
                                        }
                                    });

                                    Log.d("Tag", "DocumentSnapshot successfully updated!");
                                }
                            })
                                    .addOnFailureListener(new OnFailureListener() {
                                        @Override
                                        public void onFailure(@NonNull Exception e) {
                                            Log.w("Tag", "Error updating document", e);
                                        }
                                    });
                            Log.d("Tag","currentScore :" + currScore);
                        }

                    }
                    disableBoostButton();
                    enableCheckbox();
                }
                sk1.setProgress(sk1.getProgress()+one);
                sk2.setProgress(sk2.getProgress()+two);
                sk3.setProgress(sk3.getProgress()+three);

                if(sk1.getProgress() >= sk1.getMax()){
                    model.addId(sk1.getId());
                }
                if(sk2.getProgress() >= sk2.getMax()){
                    model.addId(sk2.getId());
                }
                if(sk3.getProgress() >= sk3.getMax()){
                    model.addId(sk3.getId());
                }
            }
            @Override
            public void onFinish() {
            }
        };


        /*
        when the boost button clicked, it checks to see which animal is selected to set that animal go extra step
        Also, when the user click boost button, it also takes points off from user's score and update the score.
         */
        boostButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (currScore >= model.getBet() / 2) {
                    if (cb1.isChecked()) {
                        currScore -= model.getBet() / 2;
                        documentReference.update("userScore", currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                            @Override
                            public void onSuccess(Void aVoid) {
                                documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                    @Override
                                    public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                        if (task.isSuccessful()) {
                                            DocumentSnapshot document = task.getResult();
                                            if (document.exists()) {
                                                Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                txtGrade.setText(document.getLong("userScore") + "");
                                            } else {
                                                Log.d("TAG", "No such document");
                                            }
                                        } else {
                                            Log.d("TAG", "get failed with ", task.getException());
                                        }
                                    }
                                });
                                Log.d("Tag", "DocumentSnapshot successfully updated!");
                            }
                        })
                                .addOnFailureListener(new OnFailureListener() {
                                    @Override
                                    public void onFailure(@NonNull Exception e) {
                                        Log.w("Tag", "Error updating document", e);
                                    }
                                });
                        Log.d("Tag", "currentScore :" + currScore);
                        sk1.setProgress(sk1.getProgress() + model.getMaxStep() * 3);
                    } else if (cb2.isChecked()) {
                        currScore -= model.getBet() / 2;
                        documentReference.update("userScore", currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                            @Override
                            public void onSuccess(Void aVoid) {
                                documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                    @Override
                                    public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                        if (task.isSuccessful()) {
                                            DocumentSnapshot document = task.getResult();
                                            if (document.exists()) {
                                                Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                                                txtGrade.setText(document.getLong("userScore") + "");
                                            } else {
                                                Log.d("TAG", "No such document");
                                            }
                                        } else {
                                            Log.d("TAG", "get failed with ", task.getException());
                                        }
                                    }
                                });

                                Log.d("Tag", "DocumentSnapshot successfully updated!");
                            }
                        })
                                .addOnFailureListener(new OnFailureListener() {
                                    @Override
                                    public void onFailure(@NonNull Exception e) {
                                        Log.w("Tag", "Error updating document", e);
                                    }
                                });
                        Log.d("Tag", "currentScore :" + currScore);
                        sk2.setProgress(sk2.getProgress() + model.getMaxStep() * 3);
                    } else {
                        currScore -= model.getBet() / 2;
                        documentReference.update("userScore", currScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                            @Override
                            public void onSuccess(Void aVoid) {

                                documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
                                    @Override
                                    public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                                        if (task.isSuccessful()) {
                                            DocumentSnapshot document = task.getResult();
                                            if (document.exists()) {
                                                Log.d("TAG", "DocumentSnapshot data: " + document.getData());

                                                txtGrade.setText(document.getLong("userScore") + "");
                                            } else {
                                                Log.d("TAG", "No such document");
                                            }
                                        } else {
                                            Log.d("TAG", "get failed with ", task.getException());
                                        }
                                    }
                                });
                                Log.d("Tag", "DocumentSnapshot successfully updated!");
                            }
                        })
                                .addOnFailureListener(new OnFailureListener() {
                                    @Override
                                    public void onFailure(@NonNull Exception e) {
                                        Log.w("Tag", "Error updating document", e);
                                    }
                                });
                        Log.d("Tag", "currentScore :" + currScore);

                        sk3.setProgress(sk3.getProgress() + model.getMaxStep() * 3);
                    }
                } else {
                    Toast.makeText(MainActivity.this,"You don't have enough money",Toast.LENGTH_LONG).show();
                }
            }
        });

        /*
        when the user click play button, it will start the count down timer
        It also disable the check boxes and turn the boost button on
         */
        btnPlay.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (currScore >= model.getBet()) {
                    if (cb1.isChecked() || cb2.isChecked() || cb3.isChecked()) {
                        btnPlay.setVisibility(View.INVISIBLE);
                        if (savedInstanceState != null) {
                            if (!(sk1.getProgress() < sk1.getMax() && sk1.getProgress() > 0
                                    || sk2.getProgress() < sk2.getMax() && sk2.getProgress() > 0
                                    || sk3.getProgress() < sk3.getMax() && sk3.getProgress() > 0)) {
                                sk1.setProgress(0);
                                sk2.setProgress(0);
                                sk3.setProgress(0);
                            }
                        } else {
                            sk1.setProgress(0);
                            sk2.setProgress(0);
                            sk3.setProgress(0);
                        }
                        model.clearRanking();
                        countDownTimer.start();
                        enableBoostButton();
                        disableCheckbox();
                    } else {
                        Toast.makeText(MainActivity.this, "check the box", Toast.LENGTH_LONG).show();
                    }
                } else {
                    Toast.makeText(MainActivity.this, "You don't have enough money", Toast.LENGTH_LONG).show();
                }
            }
        });

        /*
        this button is attached in the ranking box. When the user click this button, it will close the ranking box
         */
        btnClose.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                btnPlay.setVisibility(View.VISIBLE);
                rnkBox.setVisibility(View.INVISIBLE);
            }
        });

        /*
            when cb1 is checked, it will uncheck cb2 and cb3
         */
        cb1.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b){
                    //uncheck 2 va 3
                    cb2.setChecked(false);
                    cb3.setChecked(false);
                }
            }
        });

        /*
            when cb2 is checked, it will uncheck cb1 and cb3
         */
        cb2.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b){
                    //bo check 1 va 3
                    cb1.setChecked(false);
                    cb3.setChecked(false);
                }
            }
        });

        /*
            when cb3 is checked, it will uncheck cb1 and cb2
         */
        cb3.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(b){
                    //bo check 1 va 2
                    cb1.setChecked(false);
                    cb2.setChecked(false);
                }
            }
        });
    }

    /*
    disable boost button
     */
    private void disableBoostButton(){
        boostButton.setVisibility(View.INVISIBLE);
    }

    /*
    enable boost button
     */
    private  void enableBoostButton(){
        boostButton.setVisibility((View.VISIBLE));
    }

    /*
    disable all the checkboxes
     */
    private void disableCheckbox(){
        cb1.setEnabled(false);
        cb2.setEnabled(false);
        cb3.setEnabled(false);
        scoreSpinner.setEnabled(false);
    }

    /*
    enable all the checkboxes
     */
    private void enableCheckbox(){
        cb1.setEnabled(true);
        cb2.setEnabled(true);
        cb3.setEnabled(true);
        scoreSpinner.setEnabled(true);
    }

    private void disenableSeekBar(){
        sk1.setEnabled(false);
        sk2.setEnabled(false);
        sk3.setEnabled(false);
    }

    /*
    project all the views in xml file
     */
    private void project(){
        txtGrade = findViewById(R.id.score);
        cb1 = findViewById(R.id.checkBox1);
        cb2 = findViewById(R.id.checkBox2);
        cb3 = findViewById(R.id.checkBox3);
        sk1 = findViewById(R.id.seekBar1);
        sk2 = findViewById(R.id.seekBar2);
        sk3 = findViewById(R.id.seekBar3);
        btnPlay = findViewById(R.id.playButton);
        btnClose = findViewById(R.id.closeButton);
        rnkBox = findViewById(R.id.rankingBox);
        firstPosition = findViewById(R.id.firstPosition);
        secondPosition = findViewById(R.id.secondPosition);
        thirdPosition = findViewById(R.id.thirdPosition);
        scoreSpinner = findViewById(R.id.scoreSpinner);
        boostButton = findViewById(R.id.boostButton);
    }

    /*
    rank all 3 animals when they all arrive
     */
    private void ranking(){
        //First Place
        if(model.getPosition(sk1.getId()) == 0){
            firstPosition.setText("Tiger");
        } else if (model.getPosition(sk2.getId()) == 0){
            firstPosition.setText("Lion");
        } else{
            firstPosition.setText("Leopard");
        }

        //Second Place
        if(model.getPosition(sk1.getId()) == 1){
            secondPosition.setText("Tiger");
        } else if (model.getPosition(sk2.getId()) == 1){
            secondPosition.setText("Lion");
        } else{
            secondPosition.setText("Leopard");
        }

        //Third Place
        if(model.getPosition(sk1.getId()) == 2){
            thirdPosition.setText("Tiger");
        } else if (model.getPosition(sk2.getId()) == 2){
            thirdPosition.setText("Lion");
        } else{
            thirdPosition.setText("Leopard");
        }
        Log.d("Test",sk1.getId()+"");
        Log.d("Test",sk2.getId()+"");
        Log.d("Test",sk3.getId()+"");
    }

    /*
    create dropdown menu in the appbar
     */
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.appbar_menu_main, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Determine which menu option was chosen
        switch (item.getItemId()) {
            case R.id.action_profile:
                Intent intentToHome = new Intent(MainActivity.this,Profile.class);
                startActivity(intentToHome);
                return true;

            case R.id.action_ranking:
                Intent intentToRanking = new Intent(MainActivity.this,RankingPage.class);
                startActivity(intentToRanking);
                return true;


            default:
                return super.onOptionsItemSelected(item);
        }
    }
}
package com.example.racinganimal;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

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

import org.w3c.dom.Text;

import java.util.Map;

import javax.annotation.Nullable;

public class Profile extends AppCompatActivity {
    Button homeButton, getPointButton, getMoneyButton;
    TextView fullname, email, cardnumber, money, score;
    FirebaseAuth firebaseAuth;
    FirebaseFirestore firestore;
    String userID;
    long currentScore,currentMoney;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);

        getPointButton = findViewById(R.id.getPointButton);
        getMoneyButton= findViewById((R.id.getMoneyButton));
        fullname = findViewById(R.id.fullNameTextView);
        email = findViewById(R.id.emailTextView);
        cardnumber = findViewById(R.id.cardTextView);
        money = findViewById(R.id.moneyTextView);
        score = findViewById(R.id.scoreTextView);
        firebaseAuth = FirebaseAuth.getInstance();
        firestore = FirebaseFirestore.getInstance();


        userID = firebaseAuth.getCurrentUser().getUid();
        final DocumentReference documentReference = firestore.collection("users").document(userID);
        documentReference.addSnapshotListener(this, new EventListener<DocumentSnapshot>() {
            @Override
            public void onEvent(@Nullable DocumentSnapshot documentSnapshot, @Nullable FirebaseFirestoreException e) {
                email.setText(documentSnapshot.getString("userEmail"));
                fullname.setText(documentSnapshot.getString("userFullName"));
                cardnumber.setText(documentSnapshot.getString("userCardNumber"));
                money.setText(documentSnapshot.getLong("userMoney")+"");
                score.setText(documentSnapshot.getLong("userScore")+"");
            }
        });

        documentReference.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            @Override
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                if (task.isSuccessful()) {
                    DocumentSnapshot document = task.getResult();
                    if (document.exists()) {
                        Log.d("TAG", "DocumentSnapshot data: " + document.getData());
                        currentMoney = document.getLong("userMoney");
                        currentScore = document.getLong("userScore");
                    } else {
                        Log.d("TAG", "No such document");
                    }
                } else {
                    Log.d("TAG", "get failed with ", task.getException());
                }
            }
        });

        Log.d("TAG", ""+currentMoney + " " + currentScore);

        /*
        convert money to point and update in Firebase
        every $5.00 is equivalent to 10 points
         */
        getPointButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                currentMoney -=5;
                currentScore += 10;
                documentReference.update("userMoney",currentMoney).addOnSuccessListener(new OnSuccessListener<Void>() {
                    @Override
                    public void onSuccess(Void aVoid) {
                        Log.d("Tag", "DocumentSnapshot successfully updated!");
                    }
                    })
                        .addOnFailureListener(new OnFailureListener() {
                            @Override
                            public void onFailure(@NonNull Exception e) {
                                Log.w("Tag", "Error updating document", e);
                            }
                        });
                documentReference.update("userScore",currentScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                    @Override
                    public void onSuccess(Void aVoid) {
                        Log.d("Tag", "DocumentSnapshot successfully updated!");
                    }
                })
                        .addOnFailureListener(new OnFailureListener() {
                            @Override
                            public void onFailure(@NonNull Exception e) {
                                Log.w("Tag", "Error updating document", e);
                            }
                        });
                Log.d("Tag","currentScore :" + currentScore + " currentMonet: "+currentMoney);
        }});

        /*
        convert point to money and update in the Firebase
        every 10 point is equivalent to $5.00
         */
        getMoneyButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                currentMoney +=5;
                currentScore -= 10;
                documentReference.update("userMoney",currentMoney).addOnSuccessListener(new OnSuccessListener<Void>() {
                    @Override
                    public void onSuccess(Void aVoid) {
                        Log.d("Tag", "DocumentSnapshot successfully updated!");
                    }
                })
                        .addOnFailureListener(new OnFailureListener() {
                            @Override
                            public void onFailure(@NonNull Exception e) {
                                Log.w("Tag", "Error updating document", e);
                            }
                        });
                documentReference.update("userScore",currentScore).addOnSuccessListener(new OnSuccessListener<Void>() {
                    @Override
                    public void onSuccess(Void aVoid) {
                        Log.d("Tag", "DocumentSnapshot successfully updated!");
                    }
                })
                        .addOnFailureListener(new OnFailureListener() {
                            @Override
                            public void onFailure(@NonNull Exception e) {
                                Log.w("Tag", "Error updating document", e);
                            }
                        });
                Log.d("Tag","currentScore :" + currentScore + " currentMonet: "+currentMoney);
            }});
    }

    /*
    create the dropdown menu in the appbar
     */
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.appbar_menu_profile, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Determine which menu option was chosen
        switch (item.getItemId()) {
            case R.id.action_play:
                Intent intentToHome = new Intent(Profile.this,MainActivity.class);
                startActivity(intentToHome);
                return true;

            case R.id.action_ranking:
                Intent intentToRanking = new Intent(Profile.this,RankingPage.class);
                startActivity(intentToRanking);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

}
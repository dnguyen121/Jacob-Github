package com.example.racinganimal;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.firestore.CollectionReference;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.Query;
import com.google.firebase.firestore.QueryDocumentSnapshot;
import com.google.firebase.firestore.QuerySnapshot;

import java.util.ArrayList;

public class RankingPage extends AppCompatActivity {
    Spinner rnkSpinner;
    TextView rankingTextView;
    ListView rankingListView;
    ListView nameListView;
    ArrayList<String> optionList;
    ArrayList<String> listName;
    ArrayList<Long> listNumber;
    FirebaseAuth firebaseAuth;
    FirebaseFirestore firestore;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ranking_page);

        rnkSpinner = findViewById(R.id.rankigSpinner);
        rankingTextView = findViewById(R.id.rankingTextView);
        rankingListView = findViewById(R.id.rankingListView);
        nameListView = findViewById(R.id.nameListView);
        firebaseAuth = FirebaseAuth.getInstance();
        firestore = FirebaseFirestore.getInstance();

        optionList = new ArrayList<>();
        optionList.add("Score");
        optionList.add("Money");

        listName = new ArrayList<>();
        listNumber = new ArrayList<>();

        final CollectionReference userCollection = firestore.collection("users");

        /*
        create a dropdown menu so the user can see the ranking of players either by score or money
        if the user chooses option 'Score', it will rank all the players in Firebase based on score
        if the user chooses option 'Money', it will rank all the players in Firebase based on money
         */
        final ArrayAdapter adapter = new ArrayAdapter(RankingPage.this,R.layout.spinner_item,optionList);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        rnkSpinner.setAdapter(adapter);
        rnkSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                String option = (String) adapterView.getItemAtPosition(i);
                if(option.equals("Score")){
                    listName.clear();
                    listNumber.clear();
                    rankingTextView.setText("Score");
                    userCollection.orderBy("userScore", Query.Direction.DESCENDING).limit(5).get().addOnCompleteListener(new OnCompleteListener<QuerySnapshot>() {
                        @Override
                        public void onComplete(@NonNull Task<QuerySnapshot> task) {
                            if(task.isSuccessful()){
                                int i = 1;
                                for(QueryDocumentSnapshot document: task.getResult()){
                                    String name = document.getString("userFullName");
                                    long currentScore = document.getLong("userScore");
                                    listName.add(i + ". " +name);
                                    listNumber.add(currentScore);
                                    i++;
                                }
                                final ArrayAdapter adapterName = new ArrayAdapter(RankingPage.this,android.R.layout.simple_expandable_list_item_1,listName);
                                adapterName.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                                nameListView.setAdapter(adapterName);

                                final ArrayAdapter adapterScore = new ArrayAdapter(RankingPage.this,android.R.layout.simple_expandable_list_item_1,listNumber);
                                adapterScore.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                                rankingListView.setAdapter(adapterScore);

                                Log.d("TAG", "inside: " + listName.isEmpty() + " "+ listNumber.isEmpty());
                            }
                        }
                    });
                    Toast.makeText(RankingPage.this, option +"", Toast.LENGTH_SHORT).show();
                } else {
                    listName.clear();
                    listNumber.clear();
                    rankingTextView.setText("Money");
                    userCollection.orderBy("userMoney", Query.Direction.DESCENDING).limit(5).get().addOnCompleteListener(new OnCompleteListener<QuerySnapshot>() {
                        @Override
                        public void onComplete(@NonNull Task<QuerySnapshot> task) {
                            if(task.isSuccessful()){
                                int i = 1;
                                for(QueryDocumentSnapshot document: task.getResult()){
                                    String name = document.getString("userFullName");
                                    long currentMoney = document.getLong("userMoney");
                                    listName.add(i+". " + name);
                                    listNumber.add(currentMoney);
                                    i++;
                                }
                                final ArrayAdapter adapterName = new ArrayAdapter(RankingPage.this,android.R.layout.simple_expandable_list_item_1,listName);
                                adapterName.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                                nameListView.setAdapter(adapterName);

                                final ArrayAdapter adapterScore = new ArrayAdapter(RankingPage.this,android.R.layout.simple_expandable_list_item_1,listNumber);
                                adapterScore.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                                rankingListView.setAdapter(adapterScore);

                                Log.d("TAG", "inside: " + listName.isEmpty() + " "+ listNumber.isEmpty());
                            }
                        }
                    });
                    Toast.makeText(RankingPage.this, option +"", Toast.LENGTH_SHORT).show();
                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

    }

    /*
    create the dropdown menu in the appbar
     */
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.appbar_menu_ranking, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        // Determine which menu option was chosen
        switch (item.getItemId()) {
            case R.id.action_play:
                Intent intentToHome = new Intent(RankingPage.this,MainActivity.class);
                startActivity(intentToHome);
                return true;

            case R.id.action_profile:
                Intent intentToRanking = new Intent(RankingPage.this,Profile.class);
                startActivity(intentToRanking);
                return true;

            case R.id.action_logout:
                FirebaseAuth.getInstance().signOut();
                Intent intToMain = new Intent(RankingPage.this, LoginActivity.class);
                startActivity(intToMain);
                return true;

            default:
                return super.onOptionsItemSelected(item);
        }
    }
}
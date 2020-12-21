//package com.example.racinganimal;
////package com.example.login;
//
//import androidx.annotation.NonNull;
//import androidx.appcompat.app.AppCompatActivity;
//
//import android.content.Context;
//import android.content.Intent;
//import android.os.Bundle;
//import android.view.View;
//import android.widget.Button;
//import android.widget.EditText;
//import android.widget.TextView;
//import android.widget.Toast;
//
//import com.google.android.gms.tasks.OnCompleteListener;
//import com.google.android.gms.tasks.Task;
//import com.google.firebase.auth.AuthResult;
//import com.google.firebase.auth.FirebaseAuth;
//
//public class LoginMainActivity extends AppCompatActivity {
//    EditText emailId, password;
//    Button btnSignUp;
//    TextView tvLogin;
//    FirebaseAuth mFirebaseAuth;
//
//
//    @Override
//    protected void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        setContentView(R.layout.activity_main);
//
//        mFirebaseAuth = FirebaseAuth.getInstance();
//        emailId = findViewById(R.id.EmaileditText);
//        password = findViewById(R.id.PasswordeditText);
//        btnSignUp = findViewById(R.id.button);
//        tvLogin = findViewById(R.id.textView);
//        btnSignUp.setOnClickListener(new View.OnClickListener(){
//            @Override
//            public void onClick(View v){
//                String email = emailId.getText().toString();
//                String pwd = password.getText().toString();
//                if(email.isEmpty()){
//                    emailId.setError("Please enter your email");
//                    emailId.requestFocus();
//                }
//                else if(pwd.isEmpty()){
//                    password.setError("Please enter a password");
//                    password.requestFocus();
//                }
//                else if (email.isEmpty() && pwd.isEmpty()){
//                    Toast.makeText(LoginMainActivity.this, "Fields Are Empty!", Toast.LENGTH_SHORT).show();
//                }
//                else if(!(email.isEmpty() && pwd.isEmpty())){
//                    mFirebaseAuth.createUserWithEmailAndPassword(email,pwd).addOnCompleteListener(LoginMainActivity.this, new OnCompleteListener<AuthResult>() {
//                        @Override
//                        public void onComplete(@NonNull Task<AuthResult> task) {
//                            if(! task.isSuccessful()){
//                                Toast.makeText(LoginMainActivity.this, "Registration failed, Please Try Again", Toast.LENGTH_SHORT).show();
//                            }
//                            else{
//                                startActivity(new Intent(LoginMainActivity.this,HomeActivity.class));
//                            }
//                        }
//
//                    });
//                }
//                else{
//                    Toast.makeText(LoginMainActivity.this, "Error Occurred!", Toast.LENGTH_SHORT).show();
//                }
//            }
//
//
//        });
//
//        tvLogin.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View v){
//                Intent i = new Intent(LoginMainActivity.this,LoginActivity.class);
//                startActivity(i);
//            }
//        });
//
//
//    }
//}

package com.example.racinganimal;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.racinganimal.HomeActivity;
import com.example.racinganimal.LoginActivity;
import com.example.racinganimal.R;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.FirebaseFirestore;

import java.util.HashMap;
import java.util.Map;

public class LoginMainActivity extends AppCompatActivity {
    EditText emailId, password, fullname, cardnumber;
    Button btnSignUp;
    TextView tvLogin;
    FirebaseAuth mFirebaseAuth;
    FirebaseFirestore fStore;
    String userID;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login_main);

        fStore = FirebaseFirestore.getInstance();
        mFirebaseAuth = FirebaseAuth.getInstance();
        emailId = findViewById(R.id.EmaileditText);
        password = findViewById(R.id.PasswordeditText);
        fullname = findViewById(R.id.FullnameeditText);
        cardnumber = findViewById(R.id.CardNumbereditText);
        btnSignUp = findViewById(R.id.button);
        tvLogin = findViewById(R.id.textView);

        if(mFirebaseAuth.getCurrentUser() != null){
            startActivity(new Intent(LoginMainActivity.this, HomeActivity.class));
        }

        /*
        Sign up button: used to submit information from an user to create an account for the user in Firebase
        It also checks whether the user miss any information and inform it to the user.
         */
        btnSignUp.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                final String email = emailId.getText().toString();
                String pwd = password.getText().toString();
                final String fullName = fullname.getText().toString();
                final String cardNumber = cardnumber.getText().toString();
                final int initialMoney = 1000;
                final int initialScore = 0;
                if(email.isEmpty()){
                    emailId.setError("Please enter your email");
                    emailId.requestFocus();
                }
                else if(pwd.isEmpty()){
                    password.setError("Please enter a password");
                    password.requestFocus();
                }
                else if (email.isEmpty() && pwd.isEmpty()){
                    Toast.makeText(LoginMainActivity.this, "Fields Are Empty!", Toast.LENGTH_SHORT).show();
                }
                else if(!(email.isEmpty() && pwd.isEmpty())){
                    mFirebaseAuth.createUserWithEmailAndPassword(email,pwd).addOnCompleteListener(LoginMainActivity.this, new OnCompleteListener<AuthResult>() {
                        @Override
                        public void onComplete(@NonNull Task<AuthResult> task) {
                            if(! task.isSuccessful()){
                                Toast.makeText(LoginMainActivity.this, "Registration failed, Please Try Again", Toast.LENGTH_SHORT).show();
                            }
                            else{
                                userID = mFirebaseAuth.getCurrentUser().getUid();
                                DocumentReference documentReference = fStore.collection("users").document(userID);
                                Map<String,Object> user = new HashMap<>();
                                user.put("userFullName", fullName);
                                user.put("userEmail",email);
                                user.put("userCardNumber",cardNumber);
                                user.put("userScore",initialScore);
                                user.put("userMoney",initialMoney);
                                documentReference.set(user).addOnSuccessListener(new OnSuccessListener<Void>() {
                                    @Override
                                    public void onSuccess(Void aVoid) {
                                        Log.d("TAG", "onSuccess: user profile is created for" + userID);
                                    }
                                });
                                startActivity(new Intent(LoginMainActivity.this, HomeActivity.class));
                            }
                        }

                    });

                }
                else{
                    Toast.makeText(LoginMainActivity.this, "Error Occurred!", Toast.LENGTH_SHORT).show();
                }
            }
        });

        /*
        when the user finish registering account, he/she can hit the login button to navigate back to the Login Activity page
         */
        tvLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v){
                Intent i = new Intent(LoginMainActivity.this, LoginActivity.class);
                startActivity(i);
            }
        });
    }
}


package com.aarondunne.fypandroidapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import com.aarondunne.fypandroidapp.controller.IRegisterController;
import com.aarondunne.fypandroidapp.controller.RegisterController;
import com.aarondunne.fypandroidapp.view.IView;

import es.dmoral.toasty.Toasty;

public class RegisterActivity extends AppCompatActivity implements IView {

    IRegisterController registerController;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        final EditText newFirstName = findViewById(R.id.newFirstName);
        final EditText newLastName = findViewById(R.id.newLastName);
        final EditText newEmailID = findViewById(R.id.newEmailID);
        final EditText newUserPass = findViewById(R.id.newUserPass);
        final EditText newConfirmPass = findViewById(R.id.newConfirmPass);
        final Button loginButton = findViewById(R.id.loginButton);
        final Button registerButton = findViewById(R.id.registerButton);

        registerController = new RegisterController(this);

        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                registerController.onRegister(
                        newFirstName.getText().toString(),
                        newLastName.getText().toString(),
                        newEmailID.getText().toString(),
                        newUserPass.getText().toString(),
                        newConfirmPass.getText().toString());
            }
        });

        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(RegisterActivity.this, LoginActivity.class);
                startActivity(i);
            }
        });
    }

    @Override
    public void onRegisterSuccess(String msg) {
        Toasty.success(this, msg, Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onRegisterError(String msg) {
        Toasty.error(this, msg, Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onLoginSuccess(String msg) {

    }

    @Override
    public void onLoginError(String msg) {

    }
}

package com.aarondunne.fypandroidapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Checkable;
import android.widget.EditText;
import android.widget.Toast;
import com.aarondunne.fypandroidapp.controller.ILoginController;
import com.aarondunne.fypandroidapp.controller.LoginController;
import com.aarondunne.fypandroidapp.sql.DBHelper;
import com.aarondunne.fypandroidapp.view.IView;

import es.dmoral.toasty.Toasty;

public class LoginActivity extends AppCompatActivity implements IView {

    ILoginController loginController;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        final EditText emailID = findViewById(R.id.emailID);
        final EditText userPass = findViewById(R.id.userPass);
        final Button loginButton = findViewById(R.id.loginButton);
        final Button registerButton = findViewById(R.id.registerButton);

        loginController = new LoginController(this);

        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                DBHelper.CheckLogin checkLogin = new DBHelper.CheckLogin();
                checkLogin.execute("");
                loginController.onLogin(
                        emailID.getText().toString(),
                        userPass.getText().toString());
            }
        });

        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(LoginActivity.this, RegisterActivity.class);
                startActivity(i);
            }
        });
    }

    @Override
    public void onLoginError(String msg) {
        Toasty.error(this, msg, Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onLoginSuccess(String msg) {
        Toasty.success(this, msg, Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onRegisterSuccess(String msg) {

    }

    @Override
    public void onRegisterError(String msg) {

    }
}

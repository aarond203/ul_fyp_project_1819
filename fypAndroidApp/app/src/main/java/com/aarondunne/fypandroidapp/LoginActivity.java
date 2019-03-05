package com.aarondunne.fypandroidapp;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.aarondunne.fypandroidapp.R;
import com.aarondunne.fypandroidapp.coordinator.ILoginCoordinator;
import com.aarondunne.fypandroidapp.coordinator.LoginCoordinator;
import com.aarondunne.fypandroidapp.view.ILoginView;

import es.dmoral.toasty.Toasty;

public class LoginActivity extends AppCompatActivity implements ILoginView {

    ILoginCoordinator loginCoordinator;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        final EditText emailID = findViewById(R.id.emailID);
        final EditText userPass = findViewById(R.id.userPass);
        final Button loginButton = findViewById(R.id.loginButton);
        final Button registerButton = findViewById(R.id.registerButton);

        loginCoordinator = new LoginCoordinator(this);

        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                loginCoordinator.onLogin(emailID.getText().toString(), userPass.getText().toString());
            }
        });
    }

    @Override
    public void onLoginError(String msg) {
        Toasty.error(this, msg, Toast.LENGTH_SHORT).show();
    }

    public void onLoginSuccess(String msg) {
        Toasty.success(this, msg, Toast.LENGTH_SHORT).show();
    }
}

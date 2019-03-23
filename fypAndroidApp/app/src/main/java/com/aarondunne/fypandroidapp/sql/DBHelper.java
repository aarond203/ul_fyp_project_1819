package com.aarondunne.fypandroidapp.sql;

import android.os.AsyncTask;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.aarondunne.fypandroidapp.LoginActivity;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import es.dmoral.toasty.Toasty;

public class DBHelper {
    public ProgressBar progressBar;

    public class CheckLogin extends AsyncTask<String, String, String> {
        String msg = "";
        boolean success = false;
        String email = "", password = "";

        @Override
        protected void onPreExecute() {
            progressBar.setVisibility(View.VISIBLE);
        }

        @Override
        protected void onPostExecute(String s) {
            progressBar.setVisibility(View.GONE);
            //Toast.makeText(LoginActivity.this, s, Toast.LENGTH_LONG).show();
            if (success) {
                //Toasty.success();
            }
        }

        @Override
        protected String doInBackground(String... strings) {
            return null;
        }
    }

    public Connection connectToAzure() {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);
        Connection connection = null;
        String connectionURL;

        try {
            Class.forName("net.sorceforge.jtds.jdbc.Driver");
            connectionURL = "jdbc:jtds:sqlserver://fypsqlserver.database.windows.net:1433;DatabaseName=fypDatabase;user=aarond203@fypsqlserver;password=Qwerty@203;encrypt=true;trustServerCertificate=false;hostNameInCertificate=*.database.windows.net;loginTimeout=30;";
            connection = DriverManager.getConnection(connectionURL);
        }
        catch (SQLException se) {
            Log.e("Error: ", se.getMessage());
        }
        catch (ClassNotFoundException ce) {
            Log.e("Error: ", ce.getMessage());
        }
        catch (Exception e) {
            Log.e("Error: ", e.getMessage());
        }
        return connection;
    }
}

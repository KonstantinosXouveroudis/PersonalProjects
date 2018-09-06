/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package foodselection_jdbc;
import java.sql.*;
//simport java.io.*;
import java.util.Calendar;
import java.util.InputMismatchException;
import java.util.Scanner;
/**
 *
 * @author Kostas
 */
public class FoodSelection_JDBC {
    public static void main(String[] args) throws Exception{
        
        int option = 0;
        Scanner keyboard = new Scanner(System.in);
        
        try{
            
            //Use keyboard input to choose if we will be using today's date or a custom date.
            System.out.println("Πατήστε τον αριθμό της επιλογής σας. Επιλογές:"
                    + "\n1: Χρήση σημερινής ημερομηνίας"
                    + "\n2: Εισαγωγή ημερομηνίας από το πληκτρολόγιο");
            option = keyboard.nextInt();

            //Make sure that the user inputs an available answer (1 or 2).
            while(option != 1 && option != 2){
                System.out.println("\nΠαρακαλώ εισάγετε έγκυρη επιλογή. Επιλογές:"
                    + "\n1: Χρήση σημερινής ημερομηνίας"
                    + "\n2: Εισαγωγή ημερομηνίας από το πληκτρολόγιο");
                option = keyboard.nextInt();
            }
        }
        catch(InputMismatchException e){
            System.out.println("Mismatched input. Resetting to default date.");
            option = 1;
        }
        
        //Day and Month variables.
        int day = 0, month = 0;
        
        //If we chose to take today's date, use Calendar.
        if (option == 1){
            /* Number of days:
            Sunday = 1, Monday = 2, Tuesday = 3, Wednesday = 4, Thursday = 5, Friday = 6, Saturday = 7
            
            We add one to the month's Integer becayse Calendar.MONTH starts counting from 0.
            Therefore, by adding 1, the months are counted as in everyday life: January = 1, up until December = 12.
            */
            Calendar calendar = Calendar.getInstance();
            day = calendar.get(Calendar.DAY_OF_WEEK); 
            month = (calendar.get(Calendar.MONTH) + 1);
        }
        
        //Else, input custom date. Numbers match the ones used by Calendar.
        else if (option == 2){            
            //Using an exception in case the user inputs a number that exceeds Integer's range, or an invalid character.
            //If we catch an exception, we set the date to the current day using Calendar.
            try{
                System.out.println("\nΕισάγετε τον αντίστοιχο αριθμό της μέρας που θέλετε." 
                        + " Διαφορετικοί αριθμοί δεν θα δουλέψουν: "
                        + "\n1: Κυριακή \n2: Δευτέρα \n3: Τρίτη \n4: Τετάρτη"
                        + "\n5: Πέμπτη \n6: Παρασκευή \n7: Σάββατο");
                
                day = keyboard.nextInt();
                while(day < 1 || day > 7){
                    day = keyboard.nextInt();
                }
                
                //We do the same with the month
                System.out.println("\nΕισάγετε τον αντίστοιχο αριθμό του μήνα που θέλετε." 
                        + " Διαφορετικοί αριθμοί δεν θα δουλέψουν: "
                        + "\n1: Ιανουάριος    5: Μάϊος        9: Σεπτέμβριος"
                        + "\n2: Φεβρουάριος   6: Ιούνιος     10: Οκτώβριος"
                        + "\n3: Μάρτιος       7: Ιούλιος     11: Νοέμβριος"
                        + "\n4: Απρίλιος      8: Αύγουστος   12: Δελέμβριος");
                month = keyboard.nextInt();
                while(month < 1 || month > 12){
                    month = keyboard.nextInt();
                }
            }
            catch(InputMismatchException e){
                System.out.println("\nERROR: Mismatched input. Switching to current date instead.");
                Calendar calendar = Calendar.getInstance();
                day = calendar.get(Calendar.DAY_OF_WEEK); 
                month = (calendar.get(Calendar.MONTH) + 1);
            }
        }
        
        System.out.println(day + " " + month);
        
        Connection dbConnection;
        Statement statement;
        ResultSet resultSet;
        
        try{
                    
            /*WARN: Establishing SSL connection without server's identity verification is not recommended. 
                
            According to MySQL 5.5.45+, 5.6.26+ and 5.7.6+ requirements SSL connection must be established by
            default if explicit option isn't set. For compliance with existing applications not using SSL the 
            verifyServerCertificate property is set to 'false'. You need either to explicitly disable SSL by 
            setting useSSL=false, or set useSSL=true and provide truststore for server certificate verification.
            */
            Class.forName("com.mysql.jdbc.Driver");
            //dbConnection = DriverManager.getConnection("jdbc:mysql://127.0.0.1:3306/test", "root", "admin");
            dbConnection = DriverManager.getConnection("jdbc:mysql://localhost:3306/FoodSelection?useSSL=false", "root", "admin");
            System.out.println("Connected to database.\n");
        }
        catch (SQLException e){
            System.out.println("Could not connect to the database server.");
            System.out.println("SQLException: " + e.getMessage());
            System.out.println("SQLState: " + e.getSQLState());	
            return;
        }
            
        
        String table = null; //Variable for the table we will be selecting food from.
        
        //If it's Wednesday or Friday
        if (day == 4 || day == 6){ 
            table = "Food_Wed_Fri";
        }
        //Else if it's Saturday or Sunday
        else if (day == 7 || day == 1){ 
            table = "Food_Weekend";
        }
        //Else if it's any other day (Monday, Tuesday, Thursday)
        else{
            table = "Food_Regular";
        }
        
        int random = (int) (Math.random() * 3); //Range 0-2.
        //System.out.println("Random = " + random);
        //33% chance that we choose from one of the seasonal tables instead.
        if (random == 2){
            if (month >=4 && month <= 10){
                table = "Food_Summer";
            }
            else if (month <=3 || month >= 11){
                table = "Food_Winter";
            }
        }
        
        //Create statement
        statement = dbConnection.createStatement();       
        
        //Execute statement
        resultSet = statement.executeQuery("SELECT * FROM " + table + " ORDER BY RAND() LIMIT 1");
             
        String foodName;
        //while(resultSet.next()){}
        resultSet.next();
        foodName = resultSet.getString("Food_Name");
        System.out.println(foodName);
    
        String sideDish = null;
        
        //Σε περίπτωση που έχουμε διαλέξει 1 από τα παρακάτω φαγητά, πρόσφερε συνοδευτικό.
        switch(foodName){
            case "Κοτόπουλου στον φούρνο":
            case "Ψαρονέφρι":
            case "Μπριζόλες":
            case "Σουβλάκια":
            case "Πανσέτες":
            case "Τυγανιά":
            case "Μπιφτέκια":
            case "Σνίτσελ":
            case "Φιλέτα Μπακαλιάρου":
                
                try{
                    System.out.println("Θέλετε να το συνοδέψετε με κάτι; Πατήστε Yes(Y/y) ή No(N/n).");
                    sideDish = keyboard.nextLine();
                    while (!sideDish.equals("Y") && !sideDish.equals("y") && !sideDish.equals("N") && !sideDish.equals("n")){
                        //System.out.println("Θέλετε να το συνοδέψετε με κάτι; Πατήστε Yes(Y/y) ή No(N/n).");
                        sideDish = keyboard.nextLine();
                    }

                    if (sideDish.equals("Y") || sideDish.equals("y")){
                        resultSet = statement.executeQuery("SELECT * FROM Meat_Side_Dish ORDER BY RAND() LIMIT 1");
                        resultSet.next();
                        System.out.println(foodName + " με " + resultSet.getString("Food_Name"));
                    }
                }
                catch(InputMismatchException e){
                    System.out.println("\nERROR: Mismatched input. Δεν θα προσθεθεί συνοδευτικό.");
                }
 
            break;
                
            default: break;
        }
        
        statement.close();
        dbConnection.close();
    
    }
}

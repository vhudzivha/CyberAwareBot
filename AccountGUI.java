import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class AccountGUI extends JFrame {

    private JTextField txtUsername, txtFirstName, txtLastName, txtCell;
    private JPasswordField txtPassword;
    private JButton btnRegister, btnLogin;

    private String savedUsername;
    private String savedPassword;
    private String savedFirstName;
    private String savedLastName;
    private String savedCell;

    public AccountGUI() {
        setTitle("User Registration and Login");
        setSize(400, 350);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLayout(new GridLayout(7, 2, 5, 5));

        // UI Components
        add(new JLabel("Username:"));
        txtUsername = new JTextField();
        add(txtUsername);

        add(new JLabel("Password:"));
        txtPassword = new JPasswordField();
        add(txtPassword);

        add(new JLabel("First Name:"));
        txtFirstName = new JTextField();
        add(txtFirstName);

        add(new JLabel("Last Name:"));
        txtLastName = new JTextField();
        add(txtLastName);

        add(new JLabel("Cellphone (+code##########):"));
        txtCell = new JTextField();
        add(txtCell);

        btnRegister = new JButton("Register");
        btnLogin = new JButton("Login");

        add(btnRegister);
        add(btnLogin);

        // Register Button Action
        btnRegister.addActionListener(e -> registerUser());

        // Login Button Action
        btnLogin.addActionListener(e -> loginUser());

        setLocationRelativeTo(null);
        setVisible(true);
    }

    private void registerUser() {
        String username = txtUsername.getText();
        String password = new String(txtPassword.getPassword());
        String firstName = txtFirstName.getText();
        String lastName = txtLastName.getText();
        String cell = txtCell.getText();

        if (!checkUsername(username)) {
            JOptionPane.showMessageDialog(this, "Username must contain '_' and be at most 5 characters.", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        if (!checkPassword(password)) {
            JOptionPane.showMessageDialog(this, "Password must be 8+ chars, with uppercase, number, and special char.", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        if (!checkCell(cell)) {
            JOptionPane.showMessageDialog(this, "Cell number must start with + and contain correct digits.", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        // Save user info
        savedUsername = username;
        savedPassword = password;
        savedFirstName = firstName;
        savedLastName = lastName;
        savedCell = cell;

        JOptionPane.showMessageDialog(this, "Registration successful!");
    }

    private void loginUser() {
        String username = txtUsername.getText();
        String password = new String(txtPassword.getPassword());
        String cell = txtCell.getText();

        if (username.equals(savedUsername) && password.equals(savedPassword) && cell.equals(savedCell)) {
            JOptionPane.showMessageDialog(this, "Login Successful\nWelcome " + savedFirstName + " " + savedLastName + "!");
        } else {
            JOptionPane.showMessageDialog(this, "Login Failed\nInvalid credentials or cellphone number.", "Error", JOptionPane.ERROR_MESSAGE);
        }
    }

    private boolean checkUsername(String user) {
        return user.length() <= 5 && user.contains("_");
    }

    private boolean checkPassword(String pass) {
        boolean upper = false, number = false, special = false;

        if (pass.length() < 8) return false;

        for (char ch : pass.toCharArray()) {
            if (Character.isUpperCase(ch)) upper = true;
            else if (Character.isDigit(ch)) number = true;
            else if (!Character.isLetterOrDigit(ch)) special = true;
        }

        return upper && number && special;
    }

    private boolean checkCell(String cell) {
        return cell.matches("\\+\\d{1,3}\\d{10}");
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(AccountGUI::new);
    }
}
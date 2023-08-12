

export async function Login(userName, password) {
    try {
        const response = await fetch('api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'UserName': userName,
                'Password': password
            })

        });

        var authResponse = response.json();
        return authResponse;
    }
    catch (error) {
        console.log('Error occured while logging in: ' + error.message);
    }
}

export async function Logout() {
    try {
        const response = await fetch('api/Auth/logout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });

        var authResponse = await response.json();
        console.log(authResponse);
        return authResponse;
    }
    catch (error) {
        console.log('Error occured while logging in: ' + error.message);
    }
}

export async function Register(name, surname, userName, email, profileImage, password, retypePassword) {
    try {
        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Surname', surname);
        formData.append('Email', email);
        formData.append('UserName', userName);
        formData.append('Password', password);
        formData.append('RetypePassword', retypePassword);

        const response = await fetch('api/Users/register', {
            method: 'POST',
            body: formData

        });
        
        var authResponse = await response.json();
        return authResponse;
    }
    catch (error) {
        console.log('Error occured while registering user: ' + error.message);
    }
}

export async function CheckUserExists(userName) {
    try {
        const response = await fetch('api/Auth/checkuserexists', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'UserName': userName
            })

        }).then(response => response.json());

        return response;
    }

    catch (error) {
        console.log('Error occured while searching for user with appropriate username: ' + error.message);
    }
}

export async function CheckIsUserAuthenticated() {
    try {
        const response = await fetch('api/Auth/isuserauthenticated', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        return response;
    }
    catch (error) {
        console.log('Error occured while checking is user authenticated: ' + error.message);
        throw error;
    }
}

export async function SendVerificationCode(recipientEmail) {
    try {
        const response = await fetch('api/Emails/sendverificationcode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(recipientEmail)
        });
        
        var authResponse = await response.json();
        return authResponse;
    }
    catch (error) {
        console.log('Error occured while sending VerificationCode: ' + error.message);
    }
}

export async function CheckVerificationCode(email, verificationCode) {
    try {
        const response = await fetch('api/Emails/checkverificationcode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'Email': email,
                'VerificationCode': verificationCode
            })
        });
        
        var authResponse = await response.json();
        return authResponse;
    }
    catch (error) {
        console.log('Error occured while checking VerificationCode: ' + error.message);
    }
}


















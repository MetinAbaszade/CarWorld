
export function getUser() {
    return JSON.parse(localStorage.getItem('User'));
}

export async function Login(userName, password) {
    var response;
    try {
        await fetch('api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'UserName': userName,
                'Password': password
            })

        }).then(response => response.json())
            .then(authResponse => {
                console.log(authResponse);
                response = authResponse;
            }).catch(error => {
                console.error('Error occured while logging in:', error.message);
            });

        return response;
    }
    catch (error) {
        console.log('Error occured while logging in: ' + error.message);
    }
}

export async function Register(name, surname, userName, email, profileImage, password, retypePassword) {
    var response;
    try {
        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Surname', surname);
        formData.append('Email', email);
        formData.append('UserName', userName);
        formData.append('Password', password);
        formData.append('RetypePassword', retypePassword);

        await fetch('api/Auth/register', {
            method: 'POST',
            body: formData

        }).then(response => response.json())
            .then(authResponse => {
                response = authResponse;
            }).catch(error => {
                console.error('Error occured while registering user:', error.message);
            });

        return response;
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

export async function SendVerificationCode(recipientEmail) {
    var response;
    try {
        await fetch('api/Auth/sendverificationcode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(recipientEmail)
        }).then(response => response.json())
            .then(authResponse => {
                console.log(authResponse);
                response = authResponse;
            }).catch(error => {
                console.error('Error occured while sending VerificationCode:', error.message);
            });;

        return response;
    }
    catch (error) {
        console.log('Error occured while sending VerificationCode: ' + error.message);
    }
}

export async function CheckVerificationCode(email, verificationCode) {
    var response;
    try {
        await fetch('api/Auth/checkverificationcode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'Email': email,
                'VerificationCode': verificationCode
            })
        }).then(response => response.json())
            .then(authResponse => {
                console.log(authResponse);
                response = authResponse;
            }).catch(error => {
                console.error('Error occured while checking VerificationCode:', error.message);
            });;
        return response;
    }
    catch (error) {
        console.log('Error occured while checking VerificationCode: ' + error.message);
    }
}


















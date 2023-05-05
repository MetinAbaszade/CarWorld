

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
                "UserName": userName,
                "Password": password
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
        console.log("Error occured while logging in: " + error.message);
    }
}

export async function Register(name, surname, userName, email, profileImage, password, retypePassword) {
    var response;
    try {
        await fetch('api/Auth/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "Name": name,
                "Surname": surname,
                "Email": email,
                "UserName": userName,
                "ProfileImage": profileImage,
                "Password": password,
                "RetypePassword": retypePassword
            })

        }).then(response => response.json())
            .then(authResponse => {
                console.log(authResponse);
                response = authResponse;
            }).catch(error => {
                console.error('Error occured while registering user:', error.message);
            });

        return response;
    }
    catch (error) {
        console.log("Error occured while logging in: " + error.message);
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
                "UserName": userName
            })

        });
        console.log(response)

        if (response.ok) 
            return true;
        return false;
    }
    
    catch (error) {
        console.log("Error occured while searching for user with appropriate username: " + error.message);
    }
}


















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

        if (response.n) {
            console.log(`Error occured while searching for user with appropriate username: ${response.statusText}`);
        }


    }
    catch (error) {
        console.log("Error occured while searching for user with appropriate username: " + error.message);
    }
}


















export async function GetColors() {
    var response;
    try {
        await fetch('api/Color/getcolors', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'

        }).then(response => response.json())
            .then(authResponse => {
                console.log(authResponse);
                response = authResponse;
            }).catch(error => {
                console.error('Error occured while fetching colors:', error.message);
            });

        return response;
    }
    catch (error) {
        console.log('Error occured while fetching colors: ' + error.message);
    }
}


export async function GetFueltypes() {
    var response;
    try {
        await fetch('api/Fueltype/getfueltypes', {
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
                console.error('Error occured while fetching fueltypes:', error.message);
            });

        return response;
    }
    catch (error) {
        console.log('Error occured while fetching fueltypes: ' + error.message);
    }
}
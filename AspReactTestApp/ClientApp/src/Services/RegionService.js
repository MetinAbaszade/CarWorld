

export async function GetRegions() {
    var response;
    try {
        await fetch('api/Region/getregions', {
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
                console.error('Error occured while fetching regions:', error.message);
            });

        return response;
    }
    catch (error) {
        console.log('Error occured while fetching regions: ' + error.message);
    }
}
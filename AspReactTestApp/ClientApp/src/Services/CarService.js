

export async function GetCars(languageId, pageNumber = 1, pageSize = 20, sort = "") {
    try {
        console.log("Getdim masinlari goturmeye: " + languageId);
        const response = await fetch('api/Cars/getcars', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                'PageSize': pageSize, 
                'PageNumber': pageNumber,
                'Sort': sort, 
                'LanguageId': languageId
            }),
            credentials: 'include'
        })

        if (!response.ok) {
            let error = new Error(`HTTP error! status: ${response.status}`);
            error.status = response.status;
            throw error;
        }

        const authResponse = await response.json();
        return authResponse.$values;
    }
    catch (error) {
        console.error('Error occurred while fetching Cars:', error.message);
        throw error;
    }
}

export async function GetCarDetails(carId, languageId) {
    try {
        console.log("Aye burdayam: " + languageId);
        const response = await fetch(`api/Cars/getcardetails/${carId}/${languageId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })

        if (!response.ok) {
            let error = new Error(`HTTP error! status: ${response.status}`);
            error.status = response.status;
            throw error;
        }

        const authResponse = await response.json();
        return authResponse;
    }
    catch (error) {
        console.error('Error occurred while fetching Car Details:', error.message);
        throw error;
    }
}
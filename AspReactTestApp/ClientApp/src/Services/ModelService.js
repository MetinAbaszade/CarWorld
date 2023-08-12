

export async function GetModels(brandid) {
    try {
        const response = await fetch(`api/Model/getmodels/${brandid}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });

        if (!response.ok) {
            let error = new Error(`HTTP error! status: ${response.status}`);
            error.status = response.status;
            throw error;
        }

        const authResponse = await response.json();
        return authResponse.$values;
    }
    catch (error) {
        console.error('Error occurred while fetching Models:', error.message);
        throw error;
    }
}
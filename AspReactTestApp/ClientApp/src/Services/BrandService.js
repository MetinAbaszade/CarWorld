

export async function GetBrands() {
    try {
        const response = await fetch('api/Brand/getbrands', {
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
    } catch (error) {
        console.error('Error occurred while fetching brands:', error.message);
        throw error;
    }
}
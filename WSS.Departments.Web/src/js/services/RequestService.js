import axios from "axios";

class RequestService {
    constructor(url, setProcessing) {
        this.url = url;
        this.setProcessing = setProcessing;
        this.headers = {
            'Content-Type' : 'application/json'
        }
    }

    get(successCallback, errorCallback) {
        this.request(() => axios.get(this.url), successCallback, errorCallback)
    }

    post(data, successCallback, errorCallback) {
        this.request(() => axios.post(this.url, data, {
            headers: this.headers
        }), successCallback, errorCallback)
    }

    put(data, successCallback, errorCallback) {
        this.request(() => axios.put(this.url, data, {
            headers: this.headers
        }), successCallback, errorCallback)
    }
    
    delete(data, successCallback, errorCallback) {
        this.request(() => axios.delete(this.url, {
            headers: this.headers,
            data: data
        }), successCallback, errorCallback);
    }

    request(action, successCallback, errorCallback) {
        this.setProcessing(true);
        action()
            .then((response) => {
                successCallback(response);
            })
            .catch((error) => {
                
                if (errorCallback) errorCallback();

                const errorText = error.response.data.title || error.response.data;
                alert(`Error: ${errorText}`);
               
            })
            .then(() => {
                this.setProcessing(false);
            })
    }
}



export default RequestService;

import axios from "axios";
import FileUploader from "./FileUploader";
import React, {useState} from "react";
import {XML_IMPORT} from "../../common/urls";


const ImportXmlForm = ({rootName, updateData, setProcessing}) => {
    const [selectedFile, setSelectedFile] = useState(null);
    
    const submitForm = () => {
        setProcessing(true);
        const formData = new FormData();
        formData.append("file", selectedFile);

        axios
            .post(XML_IMPORT, formData)
            .then((res) => {
                alert(res.data);
                updateData();
            })
            .catch((error) => {
                if (error.response) {
                    alert(`Error : ${error.response.data}`);
                } else if (error.request) {
                    alert(error.request);
                } else {
                    alert(`Error : ${error.message}`);
                }
            })
            .finally(() => setProcessing(false));
    };
    
    return (
        <form className="import-form">
            <span>Import departments to {rootName} from XML: </span>
            <FileUploader
                onFileSelect={(file) => setSelectedFile(file)}
            />
            <button type="button" onClick={submitForm}>Import</button>
        </form>
    )
}

export default ImportXmlForm;
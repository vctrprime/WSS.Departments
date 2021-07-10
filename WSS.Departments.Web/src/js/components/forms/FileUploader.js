import React from 'react'

const FileUploader = ({onFileSelect}) => {
        
        const handleFileInput = (e) => {
                onFileSelect(e.target.files[0])
       }
    
        return (
                <div className="file-uploader">
                    <input accept="text/xml" type="file" onChange={handleFileInput} />
                    
                </div>
        )
}

export default FileUploader;
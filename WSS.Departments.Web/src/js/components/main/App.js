import React, { useState } from "react";
import LoadingOverlay from "react-loading-overlay";
import DepartmentsTreeList from "../tree/DepartmentsTreeList";

const App = () => {
    
      const [processing, setProcessing] = useState(true);
      
      return  (
            <div className="app-container">
                <LoadingOverlay active={processing} spinner text="Loading...">
                        <DepartmentsTreeList setProcessing={setProcessing} />
                </LoadingOverlay>
        </div>
      );
    };

export default App;
import * as React from "react";

export default function MyCommandCell(
    enterEdit,
    add,
    remove,
    save,
    cancel,
    editField
) {
    return class extends React.Component {
        
        
        render() {
            const { dataItem } = this.props;
            const renderRemoveButton = () => {
                if (dataItem.parentId !== null) {
                    return <button className="k-button" onClick={() => remove(dataItem)}>
                                Remove
                            </button>;
                } else {
                    return null;
                }
            };
            
            return dataItem[editField] ? (
                <td>
                    <button className="k-button" onClick={() => save(dataItem)}>
                        {dataItem.isNew ? "Add" : "Update"}
                    </button>
                    <button className="k-button" onClick={() => cancel(dataItem)}>
                        {dataItem.isNew ? "Discard" : "Cancel"}
                    </button>
                </td>
            ) :(
                <td>
                    <button className="k-button" onClick={() => add(dataItem)}>
                        Add Department
                    </button>
                    <button className="k-button" onClick={() => enterEdit(dataItem)}>
                        Edit
                    </button>
                    {renderRemoveButton()}
                    
                </td>
            );
        }
    };
}
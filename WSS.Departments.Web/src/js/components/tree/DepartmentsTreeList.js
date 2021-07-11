import * as React from "react";

import {
    TreeList,
    TreeListDraggableRow,
    createDataTree,
    mapTree,
    extendDataItem,
    moveTreeItem,
    modifySubItems,
    TreeListTextEditor,
    removeItems
} from "@progress/kendo-react-treelist";
import MyCommandCell from "./MyCommandCell";
import ImportXmlForm from "../forms/ImportXmlForm";
import {DEPARTMENTS, XML_EXPORT} from "../../common/urls";
import RequestService from "../../services/RequestService";
import { createNewItem, getByLevel, dataItemFromItemToSave } from "../../utils/treeListUtils";


const expandField = "expanded";
const subItemsField = "children";
const editField = "inEdit";


export default class DepartmentsTreeList extends React.Component {
    constructor(props) {
        super(props);
        this.departmentRequestService = new RequestService(DEPARTMENTS, this.props.setProcessing);
    }
    
    state = {
        data: [],
        expanded: [1],
        inEdit: [],
    };
    
    componentDidMount() {
        this._updateTreeData();
    }
    
    _updateTreeData = () => {
        const self = this;
        this.departmentRequestService.get(
            (response) => {
                response.data.forEach((value) => value.originalName = value.name);
                const dataTree = createDataTree(
                    response.data,
                    (i) => i.id,
                    (i) => i.parentId,
                    "children"
                );
                self.setState({data: [...dataTree]})}
        )
    }

    onExpandChange = (e) => {
        this.setState({
            expanded: e.value
                ? this.state.expanded.filter((id) => id !== e.dataItem.id)
                : [...this.state.expanded, e.dataItem.id],
        });
    };
    onRowDrop = (event) => {
        const self = this;
        
        const newParent = getByLevel(event.draggedOver, this.state.data);
        const item = getByLevel(event.dragged, this.state.data);
        
        if (newParent.id === item.parentId) return;
        
        item.parentId = newParent.id;
        item.name = item.originalName;
        
        this.departmentRequestService.put(item, 
           (response) => {
               item.rowVersion = response.data.rowVersion;
               
               self.setState({
                   data: moveTreeItem(
                       self.state.data,
                       event.dragged,
                       event.draggedOver,
                       subItemsField
                   )
               });
           },
           () => self._updateTreeData())
    };
    onItemChange = (event) => {
        if (event.field === "name") event.field = "originalName";
        
        this.setState({
            data: mapTree(this.state.data, subItemsField, (item) =>
                item.id === event.dataItem.id
                    ? extendDataItem(item, subItemsField, { [event.field]: event.value })
                    : item
            ),
        });
    };
    
    
    _add = (dataItem) => {
        const newRecord = createNewItem(dataItem.id);

        this.setState({
            inEdit: [...this.state.inEdit, newRecord],
            expanded: [...this.state.expanded, dataItem.id],
            data: modifySubItems(
                this.state.data,
                subItemsField,
                (item) => item.id === dataItem.id,
                (subItems) => [...subItems, newRecord]
            ),
        });
    };
    _save = (dataItem) => {
        const { isNew, inEdit, ...itemToSave } = dataItem;
        const self = this;
        
        const saveCallback = (tempId) => {
            const id = tempId || itemToSave.id;
            self.setState({
                data: mapTree(self.state.data, subItemsField, (item) =>
                    item.id === id  ? itemToSave : item
                ),
                inEdit: self.state.inEdit.filter((i) => i.id !== id),
            });
        }
        
        const entityToSave = dataItemFromItemToSave(itemToSave, isNew);
        
        if (isNew) {
            const temporaryId = itemToSave.id;
            this.departmentRequestService.post(entityToSave,
                (response) => {
                    const newItem = response.data;

                    itemToSave.rowVersion = newItem.rowVersion;
                    itemToSave.name = newItem.name;
                    itemToSave.originalName = newItem.name;
                    itemToSave.id = newItem.id;

                    saveCallback(temporaryId);
                })
        }
        else {
            this.departmentRequestService.put(entityToSave,
                (response) => {
                    itemToSave.rowVersion = response.data.rowVersion;
                    itemToSave.originalName = response.data.name;
                    saveCallback(null);
                },
                () => {
                    self.setState({
                        inEdit: self.state.inEdit.filter((i) => i.id !== itemToSave.id),
                    });
                    self._updateTreeData()
                })
        }
        
    };
    _cancel = (editedItem) => {
        const { inEdit, data } = this.state;
        if (editedItem.isNew) {
            this.setState({
                data: removeItems(
                    this.state.data,
                    subItemsField,
                    (i) => i.id === editedItem.id
                ),
                inEdit: this.state.inEdit.filter((i) => i.id !== editedItem.id),
            });
            return;
        }

        this.setState({
            data: mapTree(data, subItemsField, (item) =>
                item.id === editedItem.id ? inEdit.find((i) => i.id === item.id) : item
            ),
            inEdit: inEdit.filter((i) => i.id !== editedItem.id),
        });
    };

    _enterEdit = (dataItem) => {
        this.setState({
            inEdit: [...this.state.inEdit, extendDataItem(dataItem, subItemsField)],
        });
    };
    
    _remove = (dataItem) => {
        const self = this;
        this.departmentRequestService.delete(dataItem,
            () => {
                self.setState({
                    data: removeItems(
                        self.state.data,
                        subItemsField,
                        (i) => i.id === dataItem.id
                    ),
                    inEdit: self.state.inEdit.filter((i) => i.id !== dataItem.id),
                });
            },
            () => self._updateTreeData())
    };
    
    
    CommandCell = MyCommandCell(this._enterEdit, this._add, this._remove, this._save, this._cancel, editField);
    
    render() {
        const { data, expanded, inEdit } = this.state;
        
        const setName = (item, index) => {
            if (!Boolean(inEdit.find((i) => i.id === item.id))) {
                item.name = `${index+1}. ${item.originalName}`;
            }
            else { item.name = item.originalName }
        }
        
        let rootName;
        
        const root = getByLevel([0], data);
        if (root) {
            rootName = root.originalName;
            setName(root, 0);
        }
        
        const exportHref = `${XML_EXPORT}?fileName=${rootName}`;
        
        const mapTreeCallback = (item) => {
            if (item.children) {
                item.children.forEach((value, index) =>  {
                    setName(value, index);
                });
            }
            return extendDataItem(item, subItemsField, {
                [expandField]: expanded.includes(item.id),
                [editField]: Boolean(inEdit.find((i) => i.id === item.id)),
            });
        }
        
        
            
        
        return (
            <div className="tree-container">
                <div className="export-import-buttons">
                    <a href={exportHref} download>Export child departments for {rootName}</a>
                    <ImportXmlForm rootName={rootName} updateData={this._updateTreeData} setProcessing={this.props.setProcessing}/>
                </div>
                
                <TreeList
                    style={{ height: "calc(100% - 70px)", overflow: "auto" }}
                    data={mapTree(data, subItemsField, mapTreeCallback)}
                    editField={editField}
                    expandField={expandField}
                    subItemsField={subItemsField}
                    onExpandChange={this.onExpandChange}
                    onItemChange={this.onItemChange}
                    columns={[
                        //{ field: "point", title: "Point", expandable: true, width: 5 },
                        { field: "name", title: "Name", expandable: true, editCell: TreeListTextEditor },
                        { cell: this.CommandCell, width: 500 }
                    ]}
                    onRowDrop={this.onRowDrop}
                    row={TreeListDraggableRow}
                />
            </div>
            
        );
    }
}

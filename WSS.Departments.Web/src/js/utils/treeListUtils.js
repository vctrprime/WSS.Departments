export const createNewItem = (parentId) => {
    const timestamp = new Date().getTime();
    return { id: timestamp, parentId: parentId, isNew: true, originalName: "Новое подразделение" };
};

export const getByLevel = (levels, data) => {
    let obj = data[0];
    levels.forEach((value, index) => {
        if (index > 0) {
            obj = obj.children[value];
        }
    })
    return obj;
}

export const dataItemFromItemToSave = (itemToSave, isNew) => {
    return {
        id: isNew ? 0 : itemToSave.id,
        name: itemToSave.name,
        parentId: itemToSave.parentId,
        rowVersion: itemToSave.rowVersion
    }
}
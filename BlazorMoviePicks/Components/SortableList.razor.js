export function init(id, group, pull, put, sort, handle, filter, component, forceFallback) {
    var sortable = new Sortable(document.getElementById(id), {
        removeOnSpill: true,
        animation: 150,
        swapThreshold: 0.65,
        ghostClass: 'blue-background-class',
        group: {
            name: group,
            pull: pull || true,
            put: put
        },
        filter: filter || undefined,
        sort: sort,
        forceFallback: forceFallback,
        handle: handle || undefined,
        onUpdate: (event) => {
            // Revert the DOM to match the .NET state
            event.item.remove();
            event.to.insertBefore(event.item, event.to.childNodes[event.oldIndex]);

            // Notify .NET to update its model and re-render
            component.invokeMethodAsync('OnUpdateJS', event.oldDraggableIndex, event.newDraggableIndex);
        },
        onRemove: (event) => {
            if (event.pullMode === 'clone') {
                // Remove the clone
                event.clone.remove();
            }

            event.item.remove();
            event.from.insertBefore(event.item, event.from.childNodes[event.oldIndex]);

            // Notify .NET to update its model and re-render
            component.invokeMethodAsync('OnRemoveJS', event.oldDraggableIndex, event.newDraggableIndex);
        },
        onSpill: (event) => {
            // Revert the DOM to match the .NET state
            event.item.remove();
            event.to.insertBefore(event.item, event.to.childNodes[event.oldIndex]);

            // Notify .NET to update its model and re-render
            component.invokeMethodAsync('OnSpillJS', event.oldDraggableIndex, event.newDraggableIndex);
        },
    });
}

export function initializeVanillaTilt() {
    const elements = document.querySelectorAll('.tilt-card');
    VanillaTilt.init(elements, {
        max: 15,
        speed: 300
    });
}
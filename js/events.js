

class Helpers {
    static dotNetHelper;

    static setDotNetHelper(value) {
        Helpers.dotNetHelper = value;
    }

    static async ScrollHandle(tabText) {
        await Helpers.dotNetHelper.invokeMethodAsync('OnWindowScrolled');
    }

    static async ResizeHandle(tabText) {
        await Helpers.dotNetHelper.invokeMethodAsync('OnWindowScrolled');
    }
}

window.Helpers = Helpers;

window.addScrollEvent = () => {
    window.addEventListener('scroll', ScrollHandle);
}

window.removeScrollEvent = () => {
    window.removeEventListener('scroll', ScrollHandle);
}

window.addResizeEvent = () => {
    window.addEventListener('resize', ResizeHandle);
}

window.removeResizeEvent = () => {
    window.addEventListener('resize', ResizeHandle);
}

const ScrollHandle = () => {
    Helpers.ScrollHandle()
}

const ResizeHandle = () => {
    Helpers.ResizeHandle()
}


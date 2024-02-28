window.getHeight = function (element) {
    if (element) {
        return element.clientHeight;
    } else {
        return -1; // Element not found
    }
}

window.getWindowHeight = () => {
    return window.innerHeight;
}

window.getWindowYOffset = () => {
    return window.pageYOffset;
}

window.scrollToTop = () => {
    document.documentElement.scrollTop = 0
}

let animationIndex = 0;


window.animateImageLoaded = (imageElement, loadIndex) => {
    setTimeout(() => {
        imageElement.parentElement.classList.add('posetAnimate');
    }, 30 * loadIndex); // Adjust the delay as needed
/*    DoAnimate(movieImageArray);*/
}

window.backdropLoad = (imageElement) => {
    imageElement.classList.add('fadeIn');
}

const DoAnimate = (array) => {
    array.forEach((elm, i) => {
        console.log(i)
        setTimeout(() => {
            /*elm.classList.add('posetAnimate');*/
            console.log(i);
        }, 2200 * i); // Adjust the delay as needed
        
    });
}

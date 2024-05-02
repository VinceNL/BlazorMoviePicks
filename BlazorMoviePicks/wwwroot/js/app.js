function closeOffcanvas() {
    var offcanvasElement = document.getElementById('offcanvasSelectedMovies');
    var offcanvasInstance = bootstrap.Offcanvas.getInstance(offcanvasElement);
    if (offcanvasInstance) {
        offcanvasInstance.hide();
    }
}

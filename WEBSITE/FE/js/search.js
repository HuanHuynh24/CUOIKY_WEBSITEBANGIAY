
    let debounceTimer;
    // Hàm debounce để gọi API
    function debouncedSearch() {
        clearTimeout(debounceTimer);
    debounceTimer = setTimeout(function () {
                const query = document.getElementById("search_input").value;
    if (query) {
        console.log(query)
                    searchAPI(query);  // Gọi API sau khi người dùng dừng gõ
                }
            }, 500); // Delay 500ms trước khi gọi API (có thể thay đổi giá trị này)
        }

    // Hàm gọi API và hiển thị kết quả
    function searchAPI(query) {
            const encodedQuery = encodeURIComponent(query);
    console.log(encodedQuery)
    fetch("http://localhost:5288/api/Chitietsanpham/Searchsanpham?key=" + encodedQuery)
                .then(response => response.json())
                .then(data => {
                    if (data.ec === 0) {  // Kiểm tra mã trạng thái
        displayProducts(data.data);  // Gọi hàm hiển thị sản phẩm
                    } else {
        displayNoProductsFound();
                    }
                })
                .catch(error => {
        console.error("Error:", error);
                });
        }
    function displayProducts(products) {
        const productContainer = document.querySelector("main");
        productContainer.style.marginTop = "200px";    // Thêm margin-top
        productContainer.style.marginLeft = "10%";     // Thêm margin-left (căn giữa)
        productContainer.style.marginRight = "10%";    // Thêm margin-right (căn giữa)
        productContainer.style.width = "80%";          // Đặt width cho phần tử
        productContainer.style.minHeight = "500px";
        productContainer.style.display = "flex";

    // Xóa tất cả sản phẩm cũ
    productContainer.innerHTML = "";

            // Lặp qua các sản phẩm và tạo HTML cho từng sản phẩm
            products.forEach(product => {
                // Tạo một phần tử cho từng sản phẩm
                const productElement = document.createElement("div");
    productElement.classList.add("col-lg-3", "col-md-6");  // Đảm bảo các lớp phù hợp với bố cục của bạn

    // Thêm HTML của sản phẩm
    productElement.innerHTML = `
    <div class="single-product" onclick="window.location.href='/Home/ChitietSanpham?masanpham=${product.masanpham}&macolor=${encodeURIComponent(1)}&masize=${40}'">
        <img class="img-fluid" src="/img/product/${product.hinhanh}" alt="${product.tensanpham}">
            <div class="product-details">
                <h6>${product.tensanpham}</h6>
                <div class="price">
                    <h6>$${product.gia}</h6>
                    <h6 class="l-through">$210.00</h6>  <!-- Nếu bạn có giá cũ, có thể thay $210.00 bằng giá cũ -->
                </div>
                <div class="prd-bottom">
                    <a href="#" class="social-info">
                        <span class="ti-bag"></span>
                        <p class="hover-text">Add to bag</p>
                    </a>
                    <a href="#" class="social-info">
                        <span class="lnr lnr-heart"></span>
                        <p class="hover-text">Wishlist</p>
                    </a>
                    <a href="#" class="social-info">
                        <span class="lnr lnr-sync"></span>
                        <p class="hover-text">Compare</p>
                    </a>
                    <a href="#" class="social-info">
                        <span class="lnr lnr-move"></span>
                        <p class="hover-text">View more</p>
                    </a>
                </div>
            </div>
    </div>
    `;

    // Thêm sản phẩm vào container có lớp row
    productContainer.appendChild(productElement);
            });
        }

    function displayNoProductsFound() {
            const productContainer = document.getElementById("product-list");

    // Xóa tất cả sản phẩm cũ
    productContainer.innerHTML = "";

            // Tạo phần tử <h3> hiển thị thông báo "Không có sản phẩm"
        const noProductsElement = document.createElement("h3");
        noProductsElement.textContent = "Không có sản phẩm";
        noProductsElement.style.textAlign = "center";  // Căn giữa thông báo
        noProductsElement.style.marginTop = "20px";  // Tạo khoảng cách

        // Thêm thông báo vào container
        productContainer.appendChild(noProductsElement);
        }


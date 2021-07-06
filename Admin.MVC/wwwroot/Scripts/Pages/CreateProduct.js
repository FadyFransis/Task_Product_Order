
var app = new Vue({
    el: '#createProductVueApp',
    data: {
        categories: [],
        brands: [],
        product: { productImages: [], productSizes: [] },
        images: [],
        productSize: {},
        productSizes: [],
        productImages: [],
        language:"ar"
    },

    methods: {
     
    

    

     

        
        
        checkUserIsAuthenticated: function () {

           
            axios.get('/Account/isAuthenticated')
                .then((response) => {
                    if (response.data.result.success === true) {
                        this.addProduct();
                    }
                    else {
                        var str = "/Account/Login?returnUrl=" + window.location.pathname;
                        window.location.href = str;
                    }
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        addProduct: function () {
          
            if (!this.mainInfoForm.isValid()) {
                fireErrorAlert('أكمل البيانات الرئيسة', 'fill product main info');
                return;
            }
            axios.post(UploadUrl+'API/Product/AddProduct',
                this.product
            )
                .then(function (response) {
                    if (response.data.isError === false) {
                        window.location.href = "/Product?saved=true";
                    }
                    else
                        fireErrorAlert('خطأ فى إضافة المنتج', 'Error in adding product');
                })
                .catch(function (error) {
                    loaderHandler('hide')
                    fireErrorAlert('خطأ فى إضافة المنتج', 'Error in adding product');
                    console.log(error);
                });
            

        },

    

    
        
    },
    mounted() {
        //this.createSummerNote();
        //this.loadAllCategories();
        //this.loadAllBrands();
        this.language = document.getElementById('language').value;
    },
    computed: {
        mainInfoForm: function () {
            return $('#mainInfoForm').parsley();
        },
        productSizeForm: function () {
            return $('#productSizeForm').parsley();
        },
    }
});
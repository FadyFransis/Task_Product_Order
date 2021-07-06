
var app=new Vue({
    el: '#vueApp',
    data: {
        categories: [],
        brands: [],
        product: { productImages: [], productSizes: [] },
        images: [],
        productSize: {},
        productSizes: [],
        productImages: [],
    },

    methods: {
     
    
     
        
        loadProductInfo: function () {
            loaderHandler('show')
            var id = window.location.pathname.split('/')[window.location.pathname.split('/').length - 1];
            axios.get(UploadUrl+ 'api/Product/GetById?id=' + id)
                .then((response) => {
                    loaderHandler('hide')
                    this.product = response?.data.result;

                   
                    this.productSizes = this.product.productSizes;
                    this.productImages = this.product.productImages;
                   
                })
                .catch((error) => {
                    loaderHandler('hide')
                    console.log(error);
                });
        },
   
     
        checkUserIsAuthenticated: function () {
            
            axios.get('/Account/isAuthenticated')
                .then((response) => {
                    if (response.data.result.success === true) {
                        this.saveProduct();
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
        saveProduct: function () {
          
            loaderHandler('show');
          
            if (!this.mainInfoForm.isValid()) {
                fireErrorAlert('أكمل البيانات الرئيسة', 'fill product main info');
                loaderHandler('hide')
                return;
            }
            
            
           
            axios.post(UploadUrl+ 'API/Product/UpdateProduct',
                this.product
            )
                .then(function (response) {
                    loaderHandler('hide')
                    if (response.data.isError === false) {
                        window.location.href = "/Product?saved=true";
                    }
                    else {
                        loaderHandler('hide')
                        fireErrorAlert('خطأ فى حفظ المنتج', 'Error in saving product');
                    }
                })
                .catch(function (error) {
                    loaderHandler('hide')
                    fireErrorAlert('خطأ فى حفظ المنتج', 'Error in saving product');
                });

        },
    
 
    
    },

    mounted() {
      //  this.createSummerNote();
      //  this.loadAllCategories();
     //  this.loadAllBrands();
        
        this.loadProductInfo();
    },
    computed: {
        mainInfoForm: function () {
            return $('#mainInfoForm').parsley();
        },
       
    }
});
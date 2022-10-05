from pickle import FALSE, TRUE
import json
import scrapy

class BarboraSpider(scrapy.Spider):
    name = "barbora"
    start_urls = [
        'https://barbora.lt/',
    ]

    def parse(self, response):
        page = dict()
        increment = -1
        for category in response.css('li.b-categories-root-category'):
            category_url = category.css('a::attr(href)').get()
            category_url = response.urljoin(category_url)
            increment += 1
            page = {
                'category': category.css('a::text').get(),
                #'page-index': increment,      #an index for the order in which the page items were first initialized. Scrapy is asynchronous, so indexes are not sorted in the final list
                'categoryUrl': category_url,   #a page for a certain item type 
                'responseUrl': response.url, #a url from where this page came from
            }
            yield scrapy.Request(category_url, self.get_food_elems, meta={'page': page})

    
    def get_food_elems(self, response):
        page = response.meta['page']
        page['items'] = []
        for item in response.css('div.b-product--wrap2.b-product--desktop-grid'):
            units_1 = dict()
            units_1 = json.loads(item.css('div.b-product--wrap.clearfix.b-product--js-hook::attr(data-b-units)').get())
            units_2 = dict()
            units_2 = json.loads(item.css('div.b-product--wrap.clearfix.b-product--js-hook::attr(data-b-for-cart)').get())

            item_name = units_2['title']
            item_id = units_2['id']
            item_page_url = item.css('a::attr(href)').get()
            item_page_url = response.urljoin(item_page_url)
            item_picture_url = units_2['image']
            category_name_full_path = units_2['category_name_full_path']
            label_price = units_2['price']
            price_per_comparative_unit = units_2['comparative_unit_price']
            comparative_unit = units_2['comparative_unit']
            package_amount = units_2['quantity']
            description = None
            store_name = "Maxima"
            availability = units_2['status']
            origin_country = None
            components = None

            page['items'].append({
                'itemName': item_name,
                'itemId': item_id,
                'itemUrl': item_page_url,
                'categoryFullPath': category_name_full_path,
                'labelPrice': label_price,
                'pricePerUnit': price_per_comparative_unit,
                'units' : comparative_unit,
                'packageAmount': package_amount,
                'description': description,
                'store': store_name,
                'availability': availability,
                'origin': origin_country,
                'components': components,
                'image': item_picture_url,
            })
        yield page
    
        

export class Book {
    Title?:string
    Author?:string
    Format?:string
    ISBN?:string
    Rating?:string
    Price?:number
    OldPrice?:number
    Image?:string
    Category?:string

    makeBook(template:Book){
        this.Title = template.Title
        this.Author = template.Author
        this.Format = template.Format
        this.ISBN = template.ISBN
        this.Rating = template.Rating
        this.Price = template.Price
        this.OldPrice = template.OldPrice
        this.Image = template.Image
        this.Category = template.Category
        return this
    }
}

Create procedure [dbo].[AddNewDish]  
(  
	@UserId int,
	@Name varchar (50),  
	@Type varchar (50),  
	@Price float  
)  
as  
begin  
   Insert into Dishes values(@UserId,@Name,@Type,@Price)  
End 
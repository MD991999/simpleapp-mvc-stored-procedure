use Database_Employees
--=======SELECT=========
Alter procedure SP_GetAllProducts
/*(
@ProductID int,
@ProductName nvarchar(50),
@Price decimal(8,2),
@Qty int,
@Remarks nvarchar(50)
);*/
AS
Begin
Select * From Tblproductdetails
End

--============INSERT===============
Alter procedure SPI_InsertAlldetails(
@ProductName nvarchar(50),
@Price decimal(8,2),
@Qty int,
@Remarks nvarchar(50)
)
AS
begin
declare @RowCount int=0
--@ indicate its a variable
--declare:whenever we declare a variable we write this infront of it
--RowCount:it is the name of the variable
--int:we assign its type to int and initial value to 0.

set @RowCount=(Select count(1) from Tblproductdetails where ProductName=@ProductName)
Begin try
	begin tran
	if(@RowCount=0)
	begin
		Insert Into Tblproductdetails (ProductName,Price,Qty,Remarks)Values
		(@ProductName,@Price,@Qty,@Remarks)
		end
	commit tran
	--commit tran is used to store the changes that we have done in the database ot permanent
End try
begin catch
	rollback tran
	--once we find out that there is an error happened ,then we can bring back all changes that we have commited while doiing this call

	select ERROR_MESSAGE() --retrieves the error message associated with the error that occurred and returns it as the result of the stored procedure.
end catch
end

--update product detaisl

 --first we have to get an item by using its id

create procedure SPG_getitemByid(
@ProductID int
)
AS
Begin
Select * from Tblproductdetails where ProductID=@ProductID
End

Alter procedure SPU_UpdateAlldetails(
@ProductID int,
@ProductName nvarchar(50),
@Price decimal(8,2),
@Qty int,
@Remarks nvarchar(50)
)
AS
begin
	begin try
		begin tran
		Update Tblproductdetails set ProductName=@ProductName,Price=@Price,Qty=@Qty,Remarks=@Remarks where ProductID=@ProductID
		commit tran
	end try
begin catch
	rollback tran
	select ERROR_MESSAGE()
end catch
end

--delete userdetails

alter procedure SPD_deleteuserdetails(
@ProductID int,
@outputmessage varchar(50) output
)
AS
begin
declare @rowcount int=0;
	begin try
	 set @rowcount=(Select Count(*) from Tblproductdetails where ProductID=@ProductID);
	 if(@rowcount>0)
	 begin
		begin tran
			Delete from Tblproductdetails where ProductID=@ProductID;
			set  @outputmessage='Product successfully deleted';
		commit tran
	end
	else
	begin
		set @outputmessage='Product not available';
	end
	end try
	begin catch
	rollback tran
		Set @outputmessage=ERROR_MESSAGE();
	end catch
end
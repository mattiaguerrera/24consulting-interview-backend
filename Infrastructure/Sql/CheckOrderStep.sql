
--DECLARE @IdOrderStep INT = 3;
--DECLARE @IdOrder INT = 1;

SET NOCOUNT ON;

DECLARE @ValidationFailure TABLE (
    ErrorMessage varchar(5000)
);
  
-- Check that the order exists
INSERT INTO @ValidationFailure (ErrorMessage)
SELECT '''Order with ID ''{IdOrder}'' does not exist'
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.Orders WITH (READUNCOMMITTED)
		WHERE [IdOrder] = @IdOrder
	);

IF EXISTS (
	SELECT TOP 1 1
	FROM @ValidationFailure
	)
BEGIN
    SELECT 0 AS IsValid;
    SELECT ErrorMessage FROM @ValidationFailure;
	RETURN;
END
	
-- Check that the  
INSERT INTO @ValidationFailure (ErrorMessage)
SELECT '''The new order step ID ''{IdOrderStep}'' is not valid '
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM [dbo].[OrderStepFlows] os WITH (READUNCOMMITTED)
			INNER JOIN [dbo].[Orders] o
				ON o.OrderStepId = os.IdStepPrev
		WHERE 
			[IdOrder] = @IdOrder
			AND [IdStepNext] = @IdOrderStep
	);


IF EXISTS (
	SELECT TOP 1 1
	FROM @ValidationFailure
	)
BEGIN
    SELECT 0 AS IsValid;
    SELECT ErrorMessage FROM @ValidationFailure;
	RETURN;
END

SELECT 1 AS IsValid;

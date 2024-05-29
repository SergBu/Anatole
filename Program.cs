//Any Select Contains 
using System.IO;
using System.Reflection;

var traderTerminalTimeslotVehicleReservations = (from a in terminalTimeslotVehicleReservations
                                                 where a.Members.Any(x => x.MemberType is MemberType.CargoOwner && x.MemberId == currentMember.MemberId)
                                                       || a.Members.Any(x => x.MemberInformation.Select(x => x.CargoOwnerId).Contains(currentMember.MemberId))
                                                 select a).ToList();


//LoadAsync
restrictionAfterUpdate.DateUpdated = DateTimeHelper.UtcNow();

var entityFromDb = await context.TerminalRestrictions
    .Include(x => x.TerminalRestrictionReservation).ThenInclude(x => x.TerminalTimeslotVehicleReservations).ThenInclude(x => x.Members)
    .Include(x => x.TerminalTimeslotVehicleToRestrictions).ThenInclude(x => x.TerminalTimeslotVehicle).ThenInclude(x => x.Members)
    .Where(x => x.Id == restrictionAfterUpdate.Id).FirstOrDefaultAsync();

var entityFromDb = await context.TerminalRestrictions.FirstOrDefaultAsync(x => x.Id == restrictionAfterUpdate.Id);

if (!restriction.Shared && entityFromDb.Area?.AreaId != restriction.Area?.AreaId)
{
    entityFromDb.Area = restriction.Area;
}

await context.Entry(entityFromDb).Reference(x => x.TerminalRestrictionReservation).LoadAsync();
await context.Entry(entityFromDb).Collection(x => x.TerminalTimeslotVehicleToRestrictions).LoadAsync();
await context.Entry(entityFromDb.TerminalRestrictionReservation).Collection(x => x.TerminalTimeslotVehicleReservations).LoadAsync();
foreach (var item in entityFromDb.TerminalRestrictionReservation.TerminalTimeslotVehicleReservations)
{
    await context.Entry(item).Collection(x => x.Members).LoadAsync();
}


var entityFromDbCrops = entityFromDb.Crops.ToList();

var excludeRestrictionCropsFromRequest = restriction.Crops.Select(x => x.CropId).Except(entityFromDb.Crops.Select(x => x.CropId)).ToList();

//Select Except
&& !(x.Crops.Select(x => x.CropId).Except(cropIdsEntityFromDb)).Any()

//Collection with min values (window function)
var members = drivers.Select(x => new
{
    x.TerminalId,
    x.MemberId,
    UserId = driversInfo.SingleOrDefault(d => d.Id == x.MemberId)?.User?.Id ?? 0
}).GroupBy(x => new
{
    x.TerminalId,
    x.UserId
}).Select(group => new
{
    group.Key.TerminalId,
    group.Key.UserId,
    MemberId = group.Min(x => x.MemberId)
}).ToList();

//JoinAndSplit
string.Join(',', a.RestrictionOwners.Select(x => x.MemberId))
RestrictionOwnerIds = grouped.Key.Split(',').Select(x => Convert.ToInt32(x)).ToList()

